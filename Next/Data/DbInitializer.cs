using Next.Models;
using Microsoft.AspNetCore.Identity;
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
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{
                Id ="20dc2e4a-c9dc-44cd-88b7-1c0f7bf21028",
                    UserName ="admin@gmail.com",
                    NormalizedUserName="ADMIN@GMAIL.COM",
                    PasswordHash="AQAAAAEAACcQAAAAEHjbHnZiL6E0CxM6MWo/FA4OnDiMfh0223zrGozRfSt4aDt+G3OUkNBV9F3/ru5b6g==",
                    SecurityStamp="Y5Z6OYQEPJ74SS5VCPSPUDUJ3ZRVRTH5",
                    ConcurrencyStamp="fe1ccee7-bb12-4113-89d3-8a89abb37112",
                    isAdmin=true
            }
            };
            foreach (User c in users)
            {
                context.Users.Add(c);
            }
            context.SaveChanges();

            var DataCenters = new DataCenter[]
            {
            new DataCenter{ID="1", Country="Tel Aviv", Name="Netta" },
            new DataCenter{ID="2", Country="Singapore", Name="Santosa" },
            new DataCenter{ID="3", Country="New York", Name="Liberty"},
            new DataCenter{ID="4", Country="Moscow", Name="Puttin"},
            new DataCenter{ID="5", Country="Malaga", Name="Chuross"},

            };
            foreach (DataCenter dc in DataCenters)
            {
                context.DataCenter.Add(dc);
            }
            context.SaveChanges();
        }
    }
}

