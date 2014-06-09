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
            SqlDataReader reader = null;
            SqlCommand cmd = null;
            try
            {
                con.Open();
                cmd = new SqlCommand(cmdString, con);
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
