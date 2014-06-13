using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueuesApp.Models
{
    
    public interface IQueueRepo
    {
        IEnumerable<CustomerQueue> Get();
        CustomerQueue Get(int id);
        CustomerQueue Post(CustomerQueue q);
        CustomerQueue Put(CustomerQueue q);
        bool Delete(int id);
    }
    public class QueueManager : IQueueRepo
    {
        private QueueSQLServiceProvider servicer;
        private List<CustomerQueue> queues
        {
            get
            {
                if (HttpContext.Current.Cache["CustomerQueues"] == null)
                    HttpContext.Current.Cache["CustomerQueues"] = new List<CustomerQueue>();

                return HttpContext.Current.Cache["CustomerQueues"] as List<CustomerQueue>;
            }
            set
            {
                HttpContext.Current.Cache["CustomerQueues"] = value;
            }
        }
        public QueueManager()
        {
            servicer = new QueueSQLServiceProvider();
        }
        public IEnumerable<CustomerQueue> Get()
        {
            return queues;
        }
        public CustomerQueue Get(int id)
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
        public CustomerQueue Post( CustomerQueue q)
        {
            return Get(servicer.createBankingQueue(q));
        }
        public CustomerQueue Put( CustomerQueue q)
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
            return  (CustomerQueue)t;
        }
        public bool Delete(int id)
        {
            var u = Get(id);
            if (u == null)// not in cache
                return servicer.deleteQueue(id,QueueType.Banking);
            queues.Remove(u);
            servicer.deleteQueue(id, QueueType.Banking);
            return true;
        }


        
    }
}