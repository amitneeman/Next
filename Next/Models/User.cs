using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Next.Models
{
    public class User : IdentityUser
    {
        public User() : base()
        {
        }

        public string Country { get; set; }
        public ICollection<Server> Servers { get; set; }
        public bool isAdmin { get; set; }
    }
}
