using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QueuesApp.Models;
namespace QueuesApp.Controllers
{
    public class QueueInteractorController : ApiController
    {

        private QueueInteractor service = new QueueInteractor();
        
        
        // GET: api/QueueInteractor
        public string Get(string s)
        {
            return s;
        }

        // GET: api/QueueInteractor/5
        public string Get2(string s, int bob)
        {
            return s+bob;
        }

        // POST: api/QueueInteractor
        public string Post([FromBody]string value)
        {
            return value;
        }

        // PUT: api/QueueInteractor/5
        public string Put(int id, [FromBody]string value)
        {
            return value + id;
        }

        // DELETE: api/QueueInteractor/5
        public void Delete(int id)
        {
        }
    }
}
