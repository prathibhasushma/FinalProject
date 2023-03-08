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
    public class VehicleController : Controller
    {
        private readonly FinalProject1Context _context;

        public VehicleController(FinalProject1Context context)
        {
            _context = context;
        }

        // GET: Vehicle
        public async Task<IActionResult> Index()
        {
              return View(await _context.Vehicle.ToListAsync());
        }

        // GET: Vehicle/Details/5
        public async Task<IActionResult> Details()
        {
            //if (employee.Employee_Id == vehicles.available_seats)
            //{
            //  double  seats = vehicles.available_seats;
            //    Updates(seats);
            //}
            Employee employee;

           //var seats= await _context.Vehicle.FindAsync(employee.Vehicle_Id,vehicles.available_seats);
           // Updates(seats);
            return View(await _context.Vehicle.ToListAsync());

        }

        // GET: Vehicle/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Vehicle_Id,vehicle_capacity,available_seats")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Vehicle_Id,vehicle_capacity,available_seats")] Vehicle vehicle)
        {
            if (id != vehicle.Vehicle_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Vehicle_Id))
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
            return View(vehicle);
        }

        // GET: Vehicle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Vehicle_Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'FinalProject1Context.Vehicle'  is null.");
            }
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
          return _context.Vehicle.Any(e => e.Vehicle_Id == id);
        }

        //public async Task<IActionResult> Updates(int i )
        //{

        //    decrease(i);


        //}
        //public void decrease(int i)
        //{
        //    i--;
        //}
        public async Task<IActionResult> Updates(dynamic i)
        {

            decrease(i);
            return View("Create");

        }
        public void decrease(double i)
        {
            i--;
        }
    }
}
