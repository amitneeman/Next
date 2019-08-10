using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Next.Data;
using Next.Models;
using System.Text;
using Next.Areas.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Next.Controllers
{
    public class AdminStatisticsController : Controller
    {
        private readonly NextContext _context;

        public AdminStatisticsController(NextContext context)
        {
            _context = context;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            if (!AuthHelper.isAdmin(User, _context))
            {
                return View("_UnAuthorized");
            }
            return View();
        }

        public JsonResult OSCountStats()
        {
            List<OsCountStats> stats = new List<OsCountStats>();
            var data = _context.Servers.GroupBy(s => s.OS);
            foreach (var os in data)
            {
                stats.Add(new OsCountStats()
                {
                    Name = os.Key.ToString(),
                    Count = os.Count()
                });
            }

            return Json(stats);
            
        }

        public JsonResult ServersPerUser()
        {
            List<UserServerStats> stats = new List<UserServerStats>();
            var data = _context.Servers.Include(s => s.User).ToList().GroupBy(s => s.User);

            foreach (var user in data)
            {
                stats.Add(new UserServerStats
                {
                    User = user.Key.UserName,
                    Count = user.Count()
                });
            }
            return Json(stats);
        }
        
        public JsonResult ServerPerDC()
        {
            List<DataCenterStats> stats = new List<DataCenterStats>();
            var data = _context.Servers.Include(s => s.DataCenter).ToList().GroupBy(s => s.DataCenter);

            foreach (var datacenter in data)
            {
                stats.Add(new DataCenterStats
                {
                    Name = datacenter.Key.Name,
                    Count = datacenter.Count()
                });
            }
            return Json(stats);
        }
    }

    public class DataCenterStats
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class UserServerStats
    {
        public string User { get; set; }
        public int Count { get; set; }
    }

    public class OsCountStats
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
