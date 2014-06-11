using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueuesApp.Models
{
  public  interface ICustomerQueueWrapper
    {
     IEnumerable <User> Get();
     User Get(int id);
     CustomerQueue Put(User u);
     CustomerQueue Post(CustomerQueue u);
     bool Delete(int id);

    }

    public class QueueInteractor : ICustomerQueueWrapper
    {
        private CustomerQueue queue;
        private List<CustomerQueue> cache;
        public QueueInteractor(int id)
        {
            cache = (List<CustomerQueue>)HttpContext.Current.Cache["CustomerQueues"];
            queue = cache.Find(t=>t.id==id);
        }



        public IEnumerable<User> Get()
        {
            return queue.users;
        }

        public User Get(int id)
        {
            return null;
        }

        public CustomerQueue Put(User u)
        {
            queue.AddUser(u);
            return queue;
        }

        public CustomerQueue Post(CustomerQueue u)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            return queue.users.Remove(Get(id));
        }
    }
}