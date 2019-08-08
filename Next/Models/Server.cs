using System;
using System.Collections.Generic;

namespace Next.Models
{
	public class Server
	{
		public Server()
        {
		}

        public string ID { get; set; }
        public int CPU {get; set; }
        public int RAM { get; set; }
        public string UserID { get; set; }

        public User User { get; set; }
    }
}
