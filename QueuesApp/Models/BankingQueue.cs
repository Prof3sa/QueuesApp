using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QueuesApp.Models;

namespace QueuesApp.Models
{
    public class BankingQueue : CustomerQueue
    {

        private Dictionary<int, User> records;
        private Dictionary<User, int> positions;
        private Dictionary<User, int> swapsLeft;
        private HashSet<int> validTickets;
        private int current;
        private int nextTicket;

        public BankingQueue(int id, int ownerID) : base(id, ownerID)
        {
            records = new Dictionary<int, User>();
            positions = new Dictionary<User, int>();
            validTickets = new HashSet<int>();
            swapsLeft = new Dictionary<User, int>();
            current = 0;
            nextTicket = 1;
        }

        public void AddUser(User U)
        {
            records.Add(nextTicket, U);
            positions.Add(U, nextTicket);
            validTickets.Add(nextTicket);
            swapsLeft.Add(U, 3);
            while (validTickets.Contains(nextTicket))
                nextTicket += 1;
        }

        public int Current()
        {
            return current;
        }

        public void SignalServiceComplete(User U)
        {
            try
            {
                int pos = 0;
                positions.TryGetValue(U, out pos);
                positions.Remove(U);
                records.Remove(pos);
                swapsLeft.Remove(U);
                current = pos + 1;
            }
            catch(ArgumentException e)
            {

            }
        }

        public bool SwapCustomer(User U)
        {
            try
            {
                int swaps = 0;
                swapsLeft.TryGetValue(U, out swaps);
                if(swaps > 0)
                {
                    swapsLeft.Add(U, swaps - 1);
                    int pos = -1;
                    positions.TryGetValue(U, out pos);
                   
 
                    // If we have another customer behind U, then we swap their positions in the queue
                    if (validTickets.Contains(pos + 1))
                    {
                        User V = null;
                        records.TryGetValue(pos + 1, out  V);
                        records.Add(pos, V);
                        positions.Add(V, pos);
                        records.Add(pos + 1, U);
                        positions.Add(U, pos + 1);
                        
                    }
                    // If there is not another customer in the queue, then we make U's current space availible
                    else
                    {
                        validTickets.Remove(pos);
                        records.Remove(pos);
                        records.Add(pos + 1, U);
                        positions.Add(U, pos + 1);
                        nextTicket = pos;

                    }
                }

                return false;


            }
            catch(ArgumentNullException e)
            {
                return false;
            }
        }

        public void ClearQueue()
        {
            positions.Clear();
            validTickets.Clear();
            records.Clear();
            nextTicket = 1;
            current = 0;
        }




    }
}