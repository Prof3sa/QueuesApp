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
        // GET: api/Login
        public string Get()
        {
            JsonResponse resp = new JsonResponse();

            bool authenticated = HttpContext.Current.Session["Authenticated"] != null && (Boolean)HttpContext.Current.Session["Authenticated"] == true;
            resp.success = authenticated;
            resp.data = resp.exception = null;
            string json = JsonConvert.SerializeObject(resp, Formatting.Indented);
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

            HttpContext.Current.Session["Authenticated"] = true;

            return json;
        }


        // DELETE: api/Login/5
        public void Delete()
        {
            HttpContext.Current.Session["Authenticated"] = false;
        }
    }
}
