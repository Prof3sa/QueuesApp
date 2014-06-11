using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace QueuesApp.Models
{



    public class QueueSQLServiceProvider
    {


        private Connection con;


        public QueueSQLServiceProvider()
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


        public NormalQueue getNormalQueue(int id)
        {
            NormalQueue client = null;
            try
            {
                SqlDataReader userData = runOperation("select * from [NormalQueue] where id=" + id);
                userData.Read();
                string s = userData.GetString(2);
                client = new NormalQueue((int)userData[0], (int)userData[1], (int)userData[2], (double)userData[3], (double)userData[4]);
                //client = new User((int)userData[0], userData[1].ToString(), userData[2].ToString(), userData[3].ToString(), userData[4].ToString());
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

        public BankingQueue getBankingQueue(int id)
        {
            BankingQueue client = null;
            try
            {
                SqlDataReader userData = runOperation("select * from BankingQueue where id=" + id);
                userData.Read();
                string s = userData.GetString(2);
                client = new BankingQueue((int)userData[0], (int)userData[1], (int)userData[2], (double)userData[3], (double)userData[4]);
                //client = new User((int)userData[0], userData[1].ToString(), userData[2].ToString(), userData[3].ToString(), userData[4].ToString());
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

        public bool deleteQueue(int id, QueueType type)
        {
            string name = (type == QueueType.Banking) ? "[BankingQueue]" : "[NormalQueue]";

            try
            {
                SqlDataReader userData = runOperation("delete * from " + name + " where id=" + id);
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

        private string queueInsertString(CustomerQueue t, string tableName)
        {


            string ans = "INSERT INTO" + tableName + "(QueueOwner, servers, interarrivalTime,serviceTime) OUTPUT INSERTED.ID VALUES(";
            ans += t.ownerID + "," + t.numServers + "," + t.interarrivalTime + "," + t.serviceTime;
            ans += ")";
            return ans;
        }
        private string queueUpdateString(CustomerQueue t, string tableName)
        {


            return "update " + tableName + " SET QueueOwner =" + t.ownerID + ", servers =" + t.numServers + ", interarrivaltime=" + t.interarrivalTime + ", servicetime=" + t.serviceTime + "where id=" + t.id + ";";

        }
        public int createBankingQueue(BankingQueue q)
        {
            int id = -1;


            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = queueInsertString(q, "BankingQueue");
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


            return id;

        }

        public bool updateDatabase(BankingQueue u)
        {
            try
            {
                SqlDataReader userData = runOperation(queueUpdateString(u, "BankingQueue"));
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

        public bool updateDatabase(NormalQueue u)
        {
            try
            {
                SqlDataReader userData = runOperation(queueUpdateString(u, "NormalQueue"));
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
    }
}