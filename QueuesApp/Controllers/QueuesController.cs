using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using QueuesApp.Models;
using QueuesApp.Models;
namespace QueuesApp.Controllers
{
    public class QueuesController : ApiController
    {

        SqlConnection myConnection = new SqlConnection("user id=admin_user;" +
                                       "password=SQL123$%^;server=hbqg1lfxyv.database.windows.net,1433;" +
                                       "Trusted_Connection=no;" +
                                       "database=users; " +
                                       "connection timeout=30");
        Queues[] queues = new Queues[]
        {
            new Queues{ id=1, length=0, owner="Republic Bank", type="Type 1"},
            new Queues{ id=2, length=4, owner="Republic Bank", type="Type 1"},
            new Queues{ id=3, length=2, owner="Republic Bank", type="Type 2"}
        };

        public string GetQueues()
        {
            string result = "";
            SqlDataReader reader = null;
           
            try
            {
                myConnection.Open();
            }
            catch(Exception e)
            {
                return e.ToString();
            }
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from [queueowner]",
                                                         myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    result=result+myReader["name"].ToString();
                   
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return result;
        }
    }
}
