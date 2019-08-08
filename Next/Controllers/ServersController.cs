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
        public async Task<IActionResult> Index()
        {
            var nextContext = _context.Servers.Include(s => s.DataCenter).Include(s => s.User);
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
            ViewData["DataCenterID"] = new SelectList(_context.DataCenter, "ID", "ID");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID");
            return View();
        }

        // POST: Servers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CPU,RAM,UserID,OS,DataCenterID")] Server server)
        {
            if (ModelState.IsValid)
            {
                _context.Add(server);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DataCenterID"] = new SelectList(_context.DataCenter, "ID", "ID", server.DataCenterID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", server.UserID);
            return View(server);
        }

        // GET: Servers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var server = await _context.Servers.FindAsync(id);
            if (server == null)
            {
                return NotFound();
            }
            ViewData["DataCenterID"] = new SelectList(_context.DataCenter, "ID", "ID", server.DataCenterID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", server.UserID);
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
