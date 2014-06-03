using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;
using System.Data.SqlClient;
using QueuesApp.Models;

namespace QueuesApp.Models
{
    public class SQLServiceProvider
    {

        //protected SqlConnection con;
        //protected SqlDataReader myReader;
        //private SqlCommand myCommand;

        private Connection con;
       

        public SQLServiceProvider()
        {
            con = Connection.getInstance();
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

        



        public User getUser(int id)
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

        private string UserInsertString(string email, string firstName, string lastName, string hash)
        {
            string ans = "INSERT INTO [User](Email, Firstname, LastName, Hash) OUTPUT INSERTED.ID VALUES(";
            ans += email + "," + firstName + "," + lastName + "," + hash;
            ans += ")";
            return ans;
        }

        public User createUser(string[] s)
        {
            int id = -1;

           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = UserInsertString(s[0], s[1], s[2], s[3]);//"INSERT INTO [User](Email, Firstname, LastName, Hash) OUTPUT INSERTED.ID VALUES('Jason@gmauk.com' ,'fname','lname', 'hash')";
            cmd.Connection = this.con.getSQLConnection();

            try
            {

                con.getSQLConnection().Open();
                id = (Int32)cmd.ExecuteScalar();

            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            finally
            {
                con.getSQLConnection().Close();
            }


            return getUser(id);

        }





    }
}