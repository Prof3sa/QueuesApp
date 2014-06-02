using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QueuesApp.Models;

namespace QueuesApp.Models
{
    public abstract class CustomerQueue
    {

       // public enum QueueType { BankingQueue, NormalQueue };

        public int id { get; set; }

        public int ownerID { get; set; }

        //public string ownerName { get; set; }

        public int length { get; set; }

        public CustomerQueue(int id, int ownerID)
        {
            this.id = id;
            this.ownerID = ownerID;
            length = 0;
        }

        public abstract void AddUser(User U);

        public abstract void SignalServiceComplete(User U);

        public abstract void ClearQueue();

        



    }
}