using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QueuesApp.Models;
namespace QueuesApp.Controllers
{
    public class UserSQLServiceProviderController : ApiController
    {
        // GET: api/SQLServiceProvider
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SQLServiceProvider/5
        public User Get(int id)
        {
            return new UserSQLServiceProvider().getUser(id);
        }

        // POST: api/SQLServiceProvider
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SQLServiceProvider/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SQLServiceProvider/5
        public void Delete(int id)
        {
        }
    }
}
