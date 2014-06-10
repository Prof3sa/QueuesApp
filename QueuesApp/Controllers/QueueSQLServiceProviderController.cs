using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QueuesApp.Models;
namespace QueuesApp.Controllers
{
    public class QueueSQLServiceProviderController : ApiController
    {
        // GET: api/QueueSQLServices
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/QueueSQLServices/5
        public string Get(int id)
        {
            return new QueueSQLServiceProvider().getBankingQueue(id).ToString();
        }

        // POST: api/QueueSQLServices
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/QueueSQLServices/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/QueueSQLServices/5
        public void Delete(int id)
        {
        }
    }
}
