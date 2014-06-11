using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QueuesApp.Models;
namespace QueuesApp.Controllers
{
    public class BankingQueueController : ApiController
    {
        private readonly IBankQueueRepo _bankRepo = new BankingQueueManager();
        
        // GET: api/BankingQueue
        public IEnumerable<CustomerQueue> Get()
        {
            return _bankRepo.Get();
        }

        // GET: api/BankingQueue/5
        public BankingQueue Get(int id)
        {
            var queue = _bankRepo.Get(id);

            if (queue == null)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Task not found")
                });
            }

            return queue;
        }

        // POST: api/BankingQueue
        public HttpResponseMessage Post(BankingQueue q)
        {
            q = _bankRepo.Post(q);

            if (q == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            return Request.CreateResponse(HttpStatusCode.Created, q);
        }

        // PUT: api/BankingQueue/5
        public BankingQueue Put(BankingQueue q)
        {
            try
            {
                q = _bankRepo.Put(q);
            }
            catch (Exception)
            {

                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Task Not found")
                });
            }
            return q;
        }

        // DELETE: api/BankingQueue/5
        public HttpResponseMessage Delete(int id)
        {
            _bankRepo.Delete(id);
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent
            };
        }

     
    }
}
