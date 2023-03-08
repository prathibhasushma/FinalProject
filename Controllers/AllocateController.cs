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
    public class AllocateController : Controller
    {
        private readonly FinalProject1Context _context;

        public AllocateController(FinalProject1Context context)
        {
            _context = context;
        }
        public const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=FinalProject1.Data; Trusted_Connection=True";
        protected void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(connectionString);
        }
        // GET: Allocate
        public async Task<IActionResult> Index()
        {
            var finalProject1Context = _context.Allocate.Include(a => a.Employee).Include(a => a.Route_).Include(a => a.Vehicle);
            return View(await finalProject1Context.ToListAsync());
        }

        // GET: Allocate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var finalProject1Context = _context.Allocate.Include(a => a.Employee).Include(a => a.Route_).Include(a => a.Vehicle);
            return View(await finalProject1Context.ToListAsync());
        }

        // GET: Allocate/Create
        public IActionResult Create()
        {
            ViewData["Employee_Id"] = new SelectList(_context.Employee, "Employee_Id", "Employee_Id");
            ViewData["Route_id"] = new SelectList(_context.Route_, "Route_id", "Route_id");
            ViewData["Vehicle_Id"] = new SelectList(_context.Vehicle, "Vehicle_Id", "Vehicle_Id");
            return View();
        }

        // POST: Allocate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("allocateid,Employee_Id,Vehicle_Id,Route_id")] Allocate allocate)
        {
            var a = allocate.Vehicle_Id;
            if (ModelState.IsValid)
            {
                
                _context.Add(allocate);
                await _context.SaveChangesAsync();
                await UpdateSeat(a);
               
                return RedirectToAction(nameof(Index));
            }
            ViewData["Employee_Id"] = new SelectList(_context.Employee, "Employee_Id", "Employee_Id", allocate.Employee_Id);
            ViewData["Route_id"] = new SelectList(_context.Route_, "Route_id", "Route_id", allocate.Route_id);
            ViewData["Vehicle_Id"] = new SelectList(_context.Vehicle, "Vehicle_Id", "Vehicle_Id", allocate.Vehicle_Id);
            return View(allocate);
        }

        // GET: Allocate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Allocate == null)
            {
                return NotFound();
            }

            var allocate = await _context.Allocate.FindAsync(id);
            if (allocate == null)
            {
                return NotFound();
            }
            ViewData["Employee_Id"] = new SelectList(_context.Employee, "Employee_Id", "Employee_Id", allocate.Employee_Id);
            ViewData["Route_id"] = new SelectList(_context.Route_, "Route_id", "Route_id", allocate.Route_id);
            ViewData["Vehicle_Id"] = new SelectList(_context.Vehicle, "Vehicle_Id", "Vehicle_Id", allocate.Vehicle_Id);
            return View(allocate);
        }

        // POST: Allocate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("allocateid,Employee_Id,Vehicle_Id,Route_id")] Allocate allocate)
        {
            if (id != allocate.allocateid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allocate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllocateExists(allocate.allocateid))
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
            ViewData["Employee_Id"] = new SelectList(_context.Employee, "Employee_Id", "Employee_Id", allocate.Employee_Id);
            ViewData["Route_id"] = new SelectList(_context.Route_, "Route_id", "Route_id", allocate.Route_id);
            ViewData["Vehicle_Id"] = new SelectList(_context.Vehicle, "Vehicle_Id", "Vehicle_Id", allocate.Vehicle_Id);
            return View(allocate);
        }

        // GET: Allocate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Allocate == null)
            {
                return NotFound();
            }

            var allocate = await _context.Allocate
                .Include(a => a.Employee)
                .Include(a => a.Route_)
                .Include(a => a.Vehicle)
                .FirstOrDefaultAsync(m => m.allocateid == id);
            if (allocate == null)
            {
                return NotFound();
            }

            return View(allocate);
        }

        // POST: Allocate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Allocate == null)
            {
                return Problem("Entity set 'FinalProject1Context.Allocate'  is null.");
            }
            var allocate = await _context.Allocate.FindAsync(id);
            if (allocate != null)
            {
                _context.Allocate.Remove(allocate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllocateExists(int id)
        {
          return _context.Allocate.Any(e => e.allocateid == id);
        }
        public async Task<IActionResult> UpdateSeat(int a)
        {
            var allocate = _context.Allocate;
            using (var dbconnect = _context)
            {

                Vehicle? l = dbconnect.Vehicle.Find(a);
                double b = l.available_seats;
                l.available_seats = b-1;
                dbconnect.SaveChangesAsync();
                //l.available_seats = a;
                l.vehicle_capacity = l.available_seats;
                return View(allocate);

               
            }
        }
    }
}
