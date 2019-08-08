using Next.Models;
using System;
using System.Linq;


namespace Next.Data
{
    public static class DbInitializer
    {
        public static void Initialize(NextContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Servers.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{ID="1"},
            };
            foreach (User c in users)
            {
                context.Users.Add(c);
            }
            context.SaveChanges();

            var servers = new Server[]
            {
            new Server{ID="1",UserID="1"},
            };
            foreach (Server s in servers)
            {
                context.Servers.Add(s);
            }
            context.SaveChanges();
        }
    }
}

