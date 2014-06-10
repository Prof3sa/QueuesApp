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

        private readonly UserRepository _userRepo;

        public UserSQLServiceProviderController(UserRepository repo)
        {
            _userRepo=repo;
        }

        // GET: api/UserSQLServiceProvider
        public IEnumerable<User> Get()
        {
            return _userRepo.Get();
        }

        // GET: api/SQLServiceProvider/5
        public User Get(int id)
        {

            var user = _userRepo.Get(id);

            if (user == null)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Task not found")
                });
            }

            return user;
        }



        // POST: api/SQLServiceProvider
        public HttpResponseMessage Post([FromBody]User u)
        {
            u = _userRepo.Post(u);

            if(u==null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
           
            return Request.CreateResponse(HttpStatusCode.Created,u);
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
