﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Next.Data;
using Next.Models;
using Next.Areas.Identity;

namespace Next.Controllers
{
    public class DataCentersController : Controller
    {
        private readonly NextContext _context;

        public DataCentersController(NextContext context)
        {
            _context = context;
        }

        // GET: DataCenters
        public async Task<IActionResult> Index(string countryFilter, string nameFilter)
        {
            if (!AuthHelper.isAdmin(User, _context))
            {
                return View("_UnAuthorized");
            }
            ViewData["countryFilter"] = countryFilter;
            ViewData["nameFilter"] = nameFilter;

            //nextContext = nextContext.GroupBy(dc => dc.Name).Select(g => new { name = g.Key, count = g.Count() });
            /*var nextContext = from dc in _context.DataCenter
                              join ser in _context.Servers
                              on dc.ID equals ser.DataCenterID
                              group new { dc, ser } by new { dc.Name, dc.Country, dc.Servers.Count } into g
                              select new
                              {
                                  g.Key.Name,
                                  g.Key.Country,
                                  g.Key.Count
                              };*/


            var nextContext = from s in _context.DataCenter
                              select s;

            if (!String.IsNullOrEmpty(countryFilter))
            {
                nextContext = nextContext.Where(s => s.Country.Contains(countryFilter));
            }
            if (!String.IsNullOrEmpty(nameFilter))
            {
                nextContext = nextContext.Where(s => s.Name.Contains(nameFilter));
            }


            return View(await nextContext.ToListAsync());
        }

        // GET: DataCenters/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (!AuthHelper.isAdmin(User, _context))
            {
                return View("_UnAuthorized");
            }
            if (id == null)
            {
                return NotFound();
            }

            var dataCenter = await _context.DataCenter
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dataCenter == null)
            {
                return NotFound();
            }

            return View(dataCenter);
        }

        // GET: DataCenters/Create
        public IActionResult Create()
        {
            if (!AuthHelper.isAdmin(User, _context))
            {
                return View("_UnAuthorized");
            }
            return View();
        }

        // POST: DataCenters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Country")] DataCenter dataCenter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataCenter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dataCenter);
        }

        // GET: DataCenters/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (!AuthHelper.isAdmin(User, _context))
            {
                return View("_UnAuthorized");
            }
            if (id == null)
            {
                return NotFound();
            }

            var dataCenter = await _context.DataCenter.FindAsync(id);
            if (dataCenter == null)
            {
                return NotFound();
            }
            return View(dataCenter);
        }

        // POST: DataCenters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Name,Country")] DataCenter dataCenter)
        {
            if (id != dataCenter.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataCenter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataCenterExists(dataCenter.ID))
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
            return View(dataCenter);
        }

        // GET: DataCenters/Delete/5
        public async Task<IActionResult> Delete(string id, bool? saveChangesError = false)
        {
            if (!AuthHelper.isAdmin(User, _context))
            {
                return View("_UnAuthorized");
            }
            if (id == null)
            {
                return NotFound();
            }

            var dataCenter = await _context.DataCenter
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dataCenter == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(dataCenter);
        }


        // POST: DataCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var dataCenter = await _context.DataCenter.FindAsync(id);
            _context.DataCenter.Remove(dataCenter);
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });

            }
                   
        }

        private bool DataCenterExists(string id)
        {
            return _context.DataCenter.Any(e => e.ID == id);
        }
    }
}
