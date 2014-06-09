using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QueuesApp.Models;

namespace QueuesApp.Models 
{
    public class NormalQueue : CustomerQueue
    {
        private Dictionary<User, int> positions;
        private int nextTicket;


        public NormalQueue(int id, int ownerID, int servers, double arrivalTime, double serviceTime) : 
            base(id, ownerID, servers, arrivalTime, serviceTime)
        {
            positions = new Dictionary<User, int>();
            nextTicket = 1;
        }


        override public void AddUser(User U)
        {
            positions.Add(U, nextTicket);
            nextTicket += 1;
        }

        override public void SignalServiceComplete(User U)
        {
            positions.Remove(U);
        }

        override public void ClearQueue()
        {
            positions.Clear();
            nextTicket = 1;
        }

    }
}