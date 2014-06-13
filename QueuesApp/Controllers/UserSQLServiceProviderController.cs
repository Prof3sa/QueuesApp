using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QueuesApp.Models;
using QueuesApp.Controllers;

namespace QueuesApp.Controllers
{
    public class UserSQLServiceProviderController : ApiController
    {

        private readonly UserRepository _userRepo = new UserSQLServiceProvider();

      
        public IEnumerable<User> Get()
        {
            return _userRepo.Get();
        }

        // GET: api/SQLServiceProvider/5
        public User Get(int id)
        {

            var user = _userRepo.Get(id);
/*
          if (user == null)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Task not found")
                });
            }
            */
            return user;
        }

        

        // POST: api/SQLServiceProvider
        public string Post(UserCreate uc)
        {
            User u = new User(-1,uc.email,uc.fname,uc.lname,"fake_hash");
            u.password = uc.password;
            u = _userRepo.Post(u);

            return u != null ? "Created!" : "Failure!";
        }
        
        // PUT: api/SQLServiceProvider/5
        public User Put(UserStructure u)
        {
           User t= new User(u.id,u.email,u.fname,u.lname,u.hash);
           t.queues = u.queues;

           try 
           {
                t= _userRepo.Put(t);
            }
            catch (Exception)
            {

                
            }
            return t;
       
        }

        // DELETE: api/SQLServiceProvider/5
        public bool Delete(int id)
        {
           bool i=  _userRepo.Delete(id);

            

            return i;
        }
    }
}
