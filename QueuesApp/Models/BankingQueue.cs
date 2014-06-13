using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QueuesApp.Models;
using System.Linq.Expressions;
namespace QueuesApp.Models
{
    public class BankingQueue : CustomerQueue
    {

        private Dictionary<int, UserWrapper> records;
        private Dictionary<UserWrapper, int> positions;
        private Dictionary<UserWrapper, int> swapsLeft;
        private HashSet<int> validTickets;
        private int current;
        private int nextTicket;

        public BankingQueue(int id, int ownerID, int servers, double arrivalTime, double serviceTime) :
            base(id, ownerID, servers, arrivalTime, serviceTime)
        {
            records = new Dictionary<int, UserWrapper>();
            positions = new Dictionary<UserWrapper, int>();
            validTickets = new HashSet<int>();
            swapsLeft = new Dictionary<UserWrapper, int>();
            current = 0;
            nextTicket = 1;
        }

        override public void AddUser(User U)
        {
            UserWrapper uw = new UserWrapper(U);
            records.Add(nextTicket, uw);
            positions.Add(uw, nextTicket);
            validTickets.Add(nextTicket);
            swapsLeft.Add(uw, 3);
            while (validTickets.Contains(nextTicket))
                nextTicket += 1;
        }

        public void Skip()
        {
            try
            {
                UserWrapper currentUser = null;
                records.TryGetValue(current, out currentUser);
                SignalServiceComplete(currentUser.u);

            }
            catch(Exception e)
            {

            }
        }

        public int Current()
        {
            return current;
        }

        override public void SignalServiceComplete(User u)
        {
            UserWrapper U = new UserWrapper(u);
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

        public bool SwapCustomer(UserWrapper U)
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
                        UserWrapper V = null;
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

        override public void ClearQueue()
        {
            positions.Clear();
            validTickets.Clear();
            records.Clear();
            nextTicket = 1;
            current = 0;
        }


        override public  IEnumerable<User> getMembers() 
        {
            List<UserWrapper> temp = records.Values.ToList<UserWrapper>();
            List<User> users = new List<User>();

           temp.ForEach(delegate(UserWrapper u)
            {
                users.Add(u.u);
            });

           return users;
        }

    }
}