using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using QueuesApp.Models;
using System.Security.Cryptography;
namespace QueuesApp.Models
{


    public interface UserRepository
    {

        IEnumerable<User> Get();
        User Get(int id);
        User Post(User user);
        User Put(User user);
        bool Delete(int id);

    }
    public enum QueueType {Banking, Normal};

    public class UserSQLServiceProvider : UserRepository
    {

        //protected SqlConnection con;
        //protected SqlDataReader myReader;
        //private SqlCommand myCommand;

            private Connection con;
            private List<User> users
            {
                get
                {
                    if (HttpContext.Current.Cache["Users"] == null)
                        HttpContext.Current.Cache["Users"] = new List<User>();

                    return HttpContext.Current.Cache["Users"] as List<User>;
                }
                set
                {
                    HttpContext.Current.Cache["Users"] = value;
                }
            }
       

            public UserSQLServiceProvider()
            {
                con = Connection.getInstance();
            }


        public IEnumerable<User>Get()
            {
                return users;
            }

        public User Get(int id)
        {
            var u= users.Find(t => t.id == id);
            if(u==null)
            {
                u = getUserfromDatabase(id);
            }
            if(u!=null)
            {
                users.Add(u);
            }

            return u;
        }


        public bool userLogin( string email, string password )
        {
            User u = this.Get(email);
            if( u != null )
            {
                if (User.hashPassword(password) == u.hash) return true;
            }
            return false;
        }

        public User Get( string email )
        {
            var u = users.Find( t=> t.email == email );
            if( u == null )
            {
                u = getUserfromDatabase(email);
            }

            if (u != null) users.Add(u);

            return u;
        }

        public User Post(User user)
        {
            return createUser(user);
        }

        public User Put(User user)
        {
            var t = Get(user.id);
            if (t == null)
                throw new Exception(string.Format("User with id {0} does not exists.", user.id));
            t.email = user.email;
            t.fname = user.fname;
            t.lname = user.lname;
            t.hash = user.hash;

            updateDatabase(t);
            return t;
        }

        public bool Delete(int id)
        {
            var u = Get(id); // not in cach
            if (u == null)
                return deleteUser(id);
            users.Remove(u);
            deleteUser(id);
            return true;
        }


        // returns a list of all database users
        public IEnumerable<User>  getUsersfromDatabase(int id)
        {
            List<User> dbUsers= new List<User>();
            try
            {
                SqlDataReader userData = runOperation("select * from [user]");
                
                while(userData.Read())
                {
                dbUsers.Add( new User((int)userData[0], userData[1].ToString(), userData[2].ToString(), userData[3].ToString(),userData[4].ToString()));
                }
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            finally
            {
                if (con.getSQLConnection().State == System.Data.ConnectionState.Open)
                    con.getSQLConnection().Close();
            }
            return dbUsers;
        }
        public User getUserfromDatabase(int id)
        {
            User client = null;
            try
            {
                SqlDataReader userData = runOperation("select * from [user] where id=" + id);
                userData.Read();
                string s = userData.GetString(2);
                client = new User((int)userData[0], userData[1].ToString(),userData[2].ToString(),userData[3].ToString(),userData[4].ToString() );
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            finally
            {
                if (con.getSQLConnection().State == System.Data.ConnectionState.Open)
                    con.getSQLConnection().Close();
            }
            return client;
        }

        public User getUserfromDatabase(string email)
        {
            User client = null;
            try
            {
                SqlDataReader userData = runOperation("select * from [user] where email='" + email+"'");
                userData.Read();
                string s = userData.GetString(2);
                client = new User((int)userData[0], userData[1].ToString(), userData[2].ToString(), userData[3].ToString(),userData[4].ToString());
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            finally
            {
                if (con.getSQLConnection().State == System.Data.ConnectionState.Open)
                    con.getSQLConnection().Close();
            }
            return client;
        }

      public bool updateDatabase(User u)
        {
            try
            {
                SqlDataReader userData = runOperation("update [user] SET email =" + u.email +", FirstName =" +u.fname+ ", LastName="+ u.lname + ", Hash=" + u.hash + "where id=" + u.id);
                userData.Read();
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
                return false;
            }
            finally
            {
                if (con.getSQLConnection().State == System.Data.ConnectionState.Open)
                    con.getSQLConnection().Close();
            }

            return true;
        }

        public bool deleteUser(int id)
        {

            try
            {
                SqlDataReader userData = runOperation("delete * from [user] where id=" + id);
                userData.Read();
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
                return false;
            }
            finally
            {
                if (con.getSQLConnection().State == System.Data.ConnectionState.Open)
                    con.getSQLConnection().Close();
            }

            return true;
        }
        
        
          public static string hashPassword( string password )
        {
            var hasher = SHA1Managed.Create();
            var pass = System.Text.Encoding.Unicode.GetBytes(password);
            var hash = hasher.ComputeHash(pass);

            string hash_string = "";
            for( int i = 0; i < hash.Length; ++i )
            {
                hash_string += String.Format("{0:X2}", hash[i]);
            }

            return hash_string;

        }
        
        private string UserInsertString(User u)
        {
            string ans = "INSERT INTO [User](Email, Firstname, LastName, Hash) OUTPUT INSERTED.ID VALUES('";
            ans += u.email + "','" + u.fname + "','" + u.lname + "','" + u.hash;
            ans += "')";
            return ans;
        }

        public User createUser(User u)
        {

            if (u == null) return null;



            u.hash = UserSQLServiceProvider.hashPassword(u.password);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = UserInsertString(u);
            cmd.Connection = this.con.getSQLConnection();

            try
            {

                con.getSQLConnection().Open();
                u.id = (Int32)cmd.ExecuteScalar();

            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            finally
            {
                con.getSQLConnection().Close();
            }

            
            return Get(u.id);

        }


        private SqlDataReader runOperation(string cmdString)
        {
            SqlDataReader reader = null;
            SqlCommand cmd = null;
            try
            {
                con.getSQLConnection().Open();
                cmd = new SqlCommand(cmdString, con.getSQLConnection());
                reader = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }

            return reader;
        }



    }
}