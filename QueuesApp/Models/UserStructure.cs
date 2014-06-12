using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using QueuesApp.Models;

namespace QueuesApp.Models
{
    public class UserStructure
    {
        public int id { get; set; }
        public string email { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string hash { get; set; }

        public string password { get; set; }

     //   public IEnumerable<CustomerQueue> queues { get; set;}

    }



}
