using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using QueuesApp.Controllers;
using QueuesApp.Models;

namespace QueuesApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        public int n = 5;
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
           
        }
    }
}
