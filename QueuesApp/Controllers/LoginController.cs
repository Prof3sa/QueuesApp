using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Collections.Specialized;
using QueuesApp.Utilities;
using Newtonsoft.Json;
using QueuesApp.Models;
using QueuesApp.Controllers;


namespace QueuesApp.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Loginfcdxz
        public string Get()
        {
            return "Not Implemented";
        }

        // GET: api/Login/5
        public string Get(string email, string password)
        {
            JsonResponse resp = new JsonResponse();

            UserSQLServiceProvider usp = new UserSQLServiceProvider();
            bool success = usp.userLogin(email,password);
            resp.success =  success;
            resp.exception = (success == true) ? "" : "Invalid Login Credentials!";
            resp.data = null;
            string json = JsonConvert.SerializeObject(resp, Formatting.Indented);

            HttpContext.Current.Session["Authenticated"] = true;
            
            return json;
        }

        // POST: api/Login
        public string Post(LoginDetails loginDetails)
        {
            JsonResponse resp = new JsonResponse();

            UserSQLServiceProvider usp = new UserSQLServiceProvider();
            bool success = usp.userLogin(loginDetails.email, loginDetails.password);
            resp.success = success;
            resp.exception = (success == true) ? "" : "Invalid Login Credentials!";
            resp.data = null;
            string json = JsonConvert.SerializeObject(resp, Formatting.Indented);

            //HttpContext.Current.Session["Authenticated"] = true;

            return json;
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
