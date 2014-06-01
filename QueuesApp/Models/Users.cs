using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using QueuesApp.Models;

namespace QueuesApp.Models
{
    public class Users
    {
       private SqlConnection con;
       private SqlDataReader myReader;
       private SqlCommand myCommand; 
        public Users()
        {
            con = new SqlConnection("user id=admin_user;" +
                                     "password=SQL123$%^;server=hbqg1lfxyv.database.windows.net,1433;" +
                                     "Trusted_Connection=no;" +
                                     "database=users; " +
                                     "connection timeout=30");
        }

        private SqlDataReader runOperation(string cmdString)
        { 
            SqlDataReader reader=null;
            SqlCommand cmd = null;
            try
            {
                con.Open();
                cmd = new SqlCommand(cmdString, con);
                reader = cmd.ExecuteReader();
            }
            catch(Exception e)
            {
                Console.Write(e.ToString());
            }

            return reader;
        }
      
        public User getUser(int id)
        {
            User client=null;
            try
            {
                SqlDataReader userData = runOperation("select * from [user] where id=" + id);
                userData.Read();
                string s = userData.GetString(2);
                client = new User { id = (int)userData[0], email = userData[1].ToString(), fname = userData[2].ToString(), lname = userData[3].ToString(), hash = userData[4].ToString() };
            }
            catch(Exception e)
            {
                Console.Write(e.ToString());
            }
            finally
            {
                 if (con.State == System.Data.ConnectionState.Open) 
                    con.Close();
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
                 if (con.State == System.Data.ConnectionState.Open) 
                con.Close();
            }

            return true;
        }

       public User createUser(string [] s)
        {
            int id = -1;


               SqlCommand cmd = new SqlCommand();
               cmd.CommandType = System.Data.CommandType.Text;
               cmd.CommandText = "INSERT INTO [User](Email, Firstname, LastName, Hash) OUTPUT INSERTED.ID VALUES('Jason@gmauk.com' ,'fname','lname', 'hash')";
               cmd.Connection = this.con;

               try
               {
                   
                    con.Open();
                   id= (Int32)cmd.ExecuteScalar();
                   
               }
               catch (Exception e)
               {
                  Console.Write( e.ToString());
               }
               finally
               {
                   con.Close();
               }

               
           return getUser(id);
            
        }

        }
}
