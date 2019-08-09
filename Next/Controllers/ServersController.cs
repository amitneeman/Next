using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Next.Data;
using Next.Models;

namespace Next.Controllers
{
    public class ServersController : Controller
    {
        private readonly NextContext _context;

        public ServersController(NextContext context)
        {
            _context = context;
        }

        // GET: Servers
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["CPUSortParam"] = String.IsNullOrEmpty(sortOrder) ? "cpu_desc" : "";
            ViewData["RAMSortParam"] = sortOrder == "ram_desc" ? "ram_asc" : "ram_desc";
            ViewData["OSSortParam"] = sortOrder == "os_desc" ? "os_asc" : "os_desc";
            ViewData["DataCenterSortParam"] = sortOrder == "datacenter_desc" ? "datacenter_asc" : "datacenter_desc";   
         
            var nextContext = _context.Servers.Include(s => s.DataCenter).Include(s => s.User);

            switch (sortOrder)
            {
                case "cpu_desc":
                    nextContext = nextContext.OrderByDescending(s => s.CPU).Include(s => s.DataCenter).Include(s => s.User);
                    break;
                case "ram_desc":
                    nextContext = nextContext.OrderByDescending(s => s.RAM).Include(s => s.DataCenter).Include(s => s.User);
                    break;
                case "ram_asc":
                    nextContext = nextContext.OrderBy(s => s.RAM).Include(s => s.DataCenter).Include(s => s.User);
                    break;
                case "os_desc":
                    nextContext = nextContext.OrderByDescending(s => s.OS).Include(s => s.DataCenter).Include(s => s.User);
                    break;
                case "os_asc":
                    nextContext = nextContext.OrderBy(s => s.OS).Include(s => s.DataCenter).Include(s => s.User);
                    break;
                case "datacenter_desc":
                    nextContext = nextContext.OrderByDescending(s => s.DataCenter.ID).Include(s => s.DataCenter).Include(s => s.User);
                    break;
                case "datacenter_asc":
                    nextContext = nextContext.OrderBy(s => s.DataCenter.ID).Include(s => s.DataCenter).Include(s => s.User);
                    break;
                default:
                    nextContext = nextContext.OrderBy(s => s.CPU).Include(s => s.DataCenter).Include(s => s.User);
                    break;
            }
            return View(await nextContext.ToListAsync());
        }

        // GET: Servers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var server = await _context.Servers
                .Include(s => s.DataCenter)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (server == null)
            {
                return NotFound();
            }

            return View(server);
        }

        // GET: Servers/Create
        public IActionResult Create()
        {
            ViewData["DCName"] = new SelectList(_context.DataCenter, "ID", "Name");
            return View();
        }

        // POST: Servers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CPU,RAM,UserID,OS,DataCenterID")] Server server)
        {
            string currentUserName = User.Identity.Name;
            if(currentUserName == null)
            {
                return View("_NotLoggedIn");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == currentUserName);

            server.UserID = user.Id;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(server);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
            "Try again, and if the problem persists " +
            "see your system administrator.");
                }
            }
            ViewData["DataCenterID"] = new SelectList(_context.DataCenter, "ID", "ID", server.DataCenterID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", server.UserID);
            return View(server);
        }

        // GET: Servers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var server = await _context.Servers.SingleOrDefaultAsync(s => s.ID == id);
            if (server == null)
            {
                return NotFound();
            }


            ViewData["DataCenterID"] = new SelectList(_context.DataCenter, "ID", "Name", server.DataCenterID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", server.UserID);
            return View(server);
        }

        // POST: Servers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,CPU,RAM,UserID,OS,DataCenterID")] Server server)
        {
            if (id != server.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(server);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServerExists(server.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DataCenterID"] = new SelectList(_context.DataCenter, "ID", "ID", server.DataCenterID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", server.UserID);
            return View(server);
        }

        // GET: Servers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var server = await _context.Servers
                .Include(s => s.DataCenter)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (server == null)
            {
                return NotFound();
            }

            return View(server);
        }

        // POST: Servers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var server = await _context.Servers.FindAsync(id);
            _context.Servers.Remove(server);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServerExists(string id)
        {
            return _context.Servers.Any(e => e.ID == id);
        }
    }
}
