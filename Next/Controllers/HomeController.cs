using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Next.Data;
using Next.Models;

namespace Next.Controllers
{
    public class HomeController : Controller
    {
        private readonly NextContext _context;

        public HomeController(NextContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string currentUserName = User.Identity.Name;
            ViewData["isAdmin"] = false;
            if(currentUserName != null)
            {
                var user = _context.Users.SingleOrDefault(u => u.UserName == currentUserName);
                if (user != null && user.isAdmin)
                {
                    ViewData["isAdmin"] = true;
                }

            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
