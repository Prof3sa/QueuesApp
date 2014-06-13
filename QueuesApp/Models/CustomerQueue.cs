using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QueuesApp.Models;

namespace QueuesApp.Models
{
    public enum QueueType { Banking, Normal };
       
    public abstract class CustomerQueue
    {
        

        public int id { get; set; }

        public int ownerID { get; set; }

        //public string ownerName { get; set; }

        public int length { get; set; }

        public double estimatedWaitingTime { get; set; }
        public double interarrivalTime { get; set; }
     
        public double serviceTime { get; set; }
        public int numServers { get; set; }

        public List<User> users { get; set; }
        public CustomerQueue(int id, int ownerID, int numServers, double interarrivalTime, double serviceTime)
        {
            this.id = id;
            this.ownerID = ownerID;
            length = 0;
            computeEstimatedWaitingTime(numServers, interarrivalTime, serviceTime);

            users = new List<User>();

        }


        private void computeEstimatedWaitingTime(int numServers, double interarrivalTime, double serviceTime)
        {
            double arrivalRate = 1 / interarrivalTime;
            double serviceRate = 1 / serviceTime;
            double utilization = arrivalRate / (numServers * serviceTime);
            double p0 = 0.0;
            double diff = 1 - utilization;

            int[] factorial = new int[numServers + 1];
            factorial[0] = 1;
            for (int num = 1; num <= numServers; num++)
            {
                factorial[num] = num * factorial[num - 1];
            }

            for (int servers = 0; servers < numServers ; servers++)
            {
                p0 += Math.Pow(numServers * utilization, servers) / factorial[servers];
            }

            double prod = Math.Pow(numServers * utilization, numServers);

            p0 += (1 / diff) * (1 / factorial[numServers]) * 
                prod;
            p0 = 1 / p0;
            double pInf = (prod * p0) / (factorial[numServers] * diff);
            
            double queueLength = numServers * utilization + utilization * pInf / diff;

            estimatedWaitingTime = (queueLength / arrivalRate) - (1 / serviceRate);


        }

        public abstract void AddUser(User U);

        public abstract void SignalServiceComplete(User U);

        public abstract void ClearQueue();

      



    }
}