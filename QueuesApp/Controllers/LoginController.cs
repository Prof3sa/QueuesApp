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
namespace QueuesApp.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(string username, string password)
        {
            JsonResponse resp = new JsonResponse();
            resp.success = true;
            resp.exception = "";
            UserSQLServiceProviderController t = new UserSQLServiceProviderController();
            resp.data = t.Get(1);
            string json = JsonConvert.SerializeObject(resp, Formatting.Indented);
            return json;
        }

        // POST: api/Login
        public string Post([FromBody]string value)
        {
            NameValueCollection parameters = HttpUtility.ParseQueryString(value);
            return parameters["password"];
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
