using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueuesApp.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string hash { get; set; }
    }
}