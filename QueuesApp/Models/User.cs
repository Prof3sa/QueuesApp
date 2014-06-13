using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace QueuesApp.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string hash { get; set; }

        public string password { get; set; }

        public string queues { get; set; }

        public User(int id, string email, string firstName, string lastName, string hash)
        {
            this.id = id;
            this.email = email;
            this.fname = firstName;
            this.lname = lastName;
            this.hash = hash;
            this.queues = "";
        }

       
        public static string hashPassword( string password )
        {
            var hasher = SHA1Managed.Create();
            var pass = System.Text.Encoding.Unicode.GetBytes(password);
            var hash = hasher.ComputeHash(pass);

            string hash_string = "";
            for( int i = 0; i < hash.Length; ++i )
            {
                hash_string += String.Format("{0:X2}", hash[i]);
            }

            return hash_string;

        }


        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}