using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject1.Data;
using FinalProject1.Models;

namespace FinalProject1.Controllers
{
    public class Route_Controller : Controller
    {
        private readonly FinalProject1Context _context;

        public Route_Controller(FinalProject1Context context)
        {
            _context = context;
        }

        // GET: Route_
        public async Task<IActionResult> Index()
        {
            var finalProject1Context = _context.Route_.Include(r => r.Vehicle);
            return View(await finalProject1Context.ToListAsync());
        }

        // GET: Route_/Details/5
        public async Task<IActionResult> Details()
        {


            return View(await _context.Route_.ToListAsync());
        }

        // GET: Route_/Create
        public IActionResult Create()
        {
            ViewData["Vehicle_Id"] = new SelectList(_context.Vehicle, "Vehicle_Id", "Vehicle_Id");
            return View();
        }

        // POST: Route_/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Route_id,Vehicle_Id,routes")] Route_ route_)
        {
            if (ModelState.IsValid)
            {
                _context.Add(route_);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Vehicle_Id"] = new SelectList(_context.Vehicle, "Vehicle_Id", "Vehicle_Id", route_.Vehicle_Id);
            return View(route_);
        }

        // GET: Route_/Edit/5
        public async Task<IActionResult> Edit(double? id)
        {
            if (id == null || _context.Route_ == null)
            {
                return NotFound();
            }

            var route_ = await _context.Route_.FindAsync(id);
            if (route_ == null)
            {
                return NotFound();
            }
            ViewData["Vehicle_Id"] = new SelectList(_context.Vehicle, "Vehicle_Id", "Vehicle_Id", route_.Vehicle_Id);
            return View(route_);
        }

        // POST: Route_/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(double id, [Bind("Route_id,Vehicle_Id,routes")] Route_ route_)
        {
            if (id != route_.Route_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(route_);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Route_Exists(route_.Route_id))
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
            ViewData["Vehicle_Id"] = new SelectList(_context.Vehicle, "Vehicle_Id", "Vehicle_Id", route_.Vehicle_Id);
            return View(route_);
        }

        // GET: Route_/Delete/5
        public async Task<IActionResult> Delete(double? id)
        {
            if (id == null || _context.Route_ == null)
            {
                return NotFound();
            }

            var route_ = await _context.Route_
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.Route_id == id);
            if (route_ == null)
            {
                return NotFound();
            }

            return View(route_);
        }

        // POST: Route_/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(double id)
        {
            if (_context.Route_ == null)
            {
                return Problem("Entity set 'FinalProject1Context.Route_'  is null.");
            }
            var route_ = await _context.Route_.FindAsync(id);
            if (route_ != null)
            {
                _context.Route_.Remove(route_);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Route_Exists(double id)
        {
          return _context.Route_.Any(e => e.Route_id == id);
        }
    }
}
