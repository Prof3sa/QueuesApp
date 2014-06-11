using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueuesApp.Models
{

    public interface IBankQueueRepo
    {
        IEnumerable<BankingQueue> Get();
        BankingQueue Get(int id);
        BankingQueue Post(BankingQueue q);
        BankingQueue Put(BankingQueue q);
        bool Delete(int id);
    }
    public class BankingQueueManager : IBankQueueRepo
    {
        private QueueSQLServiceProvider servicer;

        
        private List<BankingQueue> queues
        {
            get
            {
                if (HttpContext.Current.Cache["BankingQueues"] == null)
                    HttpContext.Current.Cache["BankingQueues"] = new List<BankingQueue>();

                return HttpContext.Current.Cache["BankingQueues"] as List<BankingQueue>;
            }
            set
            {
                HttpContext.Current.Cache["BankingQueues"] = value;
            }
        }
        public BankingQueueManager()
        {
            servicer = new QueueSQLServiceProvider();
        }
        public IEnumerable<BankingQueue> Get()
        {
            return queues;
        }
        public BankingQueue Get(int id)
        {
            var u = queues.Find(t => t.id == id);
            if (u == null)
            {
                u = servicer.getBankingQueue(id);
            }
            if (u != null)
            {
                queues.Add(u);
            }

            return u;
        }
        public BankingQueue Post( BankingQueue q)
        {
            return Get(servicer.createBankingQueue(q));
        }
        public BankingQueue Put( BankingQueue q)
        {
            CustomerQueue c = (CustomerQueue)q;
            var t = Get(q.id);
            if (t == null)
                throw new Exception(string.Format("User with id {0} does not exists.", q.id));
            t.ownerID = c.ownerID;
            t.numServers = c.numServers;
            t.serviceTime = c.serviceTime;
            t.interarrivalTime = c.interarrivalTime;
            servicer.updateDatabase(t);
            return  (BankingQueue)t;
        }
        public bool Delete(int id)
        {
            var u = Get(id);
            if (u == null)// not in cache
                return servicer.deleteQueue(id, QueueType.Banking);
            queues.Remove(u);
            servicer.deleteQueue(id, QueueType.Banking);
            return true;
        }


        
    }
}