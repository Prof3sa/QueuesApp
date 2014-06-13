using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueuesApp.Models
{
    public class UserWrapper
    {
        public User u { get; set; }

        public DateTime entrytime { get; set; }

        public  string fname
        {
            get
            {
                return this.u.fname;
            }
            set
            {
                this.u.fname = value;
            }
        }
        public string lname
        {
            get
            {
                return this.u.lname;
            }
            set
            {
                this.u.lname = value;
            }

        }
        public string hash
        {
            get
            {
                return this.u.hash;
            }
            set
            {
                this.u.hash = value;
            }
        }
        public string password
        {
            get
            {
                return this.u.password;
            }
            set
            {
                this.u.password = value;
            }
        }
        public int id
        {
            get
            {
                return this.u.id;
            }
            set
            {
                u.id=value;
            }
        }
        public string queues
        {
            get
            {
                return this.u.queues;
            }
            set
            {
                this.u.queues = value;
            }
        }
        
        
        
        
        
        
        
        
        
        
        
        public UserWrapper(User u)
        {
            this.u = u;
        }
       
        public TimeSpan age()
        {
            return DateTime.Now - this.entrytime;
        }

        
    }
}