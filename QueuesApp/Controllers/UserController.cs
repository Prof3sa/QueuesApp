using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QueuesApp.Models;


namespace QueuesApp.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        public User Get(int id)
        {
            Users set = new Users();
            return set.getUser(id);
            
           
        }

        // POST: api/User
        public User Post([FromBody]string value)
        {   Users set = new Users();
            string[] s = value.Split(' ');
            return set.createUser(s);
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
            string[] s = value.Split(' ');
            Users set = new Users();

        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
            Users set = new Users();
            set.deleteUser(id);
        }
    }
}
