using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace QueuesApp.Models
{
    public class Connection
    {
        private SqlConnection myConnection;

        private const String connString = "user id=admin_user;" +
                                     "password=SQL123$%^;server=hbqg1lfxyv.database.windows.net,1433;" +
                                     "Trusted_Connection=no;" +
                                     "database=users; " +
                                     "connection timeout=30";

        private static Connection conn = null;

        public static Connection getInstance()
        {
            if (conn == null)
            {
                conn = new Connection();
            }

            return conn;
        }


        private Connection()
        {
            myConnection = new SqlConnection(connString);
        }

        public SqlConnection getSQLConnection()
        {
            return myConnection;
        }
    }
    
}
