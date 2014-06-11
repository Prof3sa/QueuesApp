using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueuesApp.Models
{
    public class UserCreate
    {
        public string email {set; get;}
        public string fname { set; get; }
        public string lname { set; get; }
        public string password { set; get; }
    }
}