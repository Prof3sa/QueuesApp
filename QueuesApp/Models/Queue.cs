using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueuesApp.Models
{
    public class Queue   {
        public int id { get; set; }
        public string owner { get; set; }
        public int length { get; set; }
        public string type { get; set; }

      

    }
}