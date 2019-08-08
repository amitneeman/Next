using System;
using System.Collections.Generic;

namespace Next.Models
{
    public class User
    {
        public User()
        {
        }

        public string ID { get; set; }
        public string Username { get; set; }
        public bool isAdmin { get; set; }
        public ICollection<Server> Servers { get; set; }
    }
}
