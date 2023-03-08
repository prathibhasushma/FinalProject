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
    public class EmployeeController : Controller
    {
        private readonly FinalProject1Context _context;

        public EmployeeController(FinalProject1Context context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            var finalProject1Context = _context.Employee.Include(e => e.Vehicle);
            return View(await finalProject1Context.ToListAsync());
        }

        // GET: Employee/Details
        public async Task<IActionResult> Details()
        {
            var finalProject1Context = _context.Employee.Include(e => e.Vehicle);
            return View(await finalProject1Context.ToListAsync());



        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            ViewData["Vehicle_Id"] = new SelectList(_context.Set<Vehicle>(), "Vehicle_Id", "Vehicle_Id");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Employee_Id,Employee_Name,Age,Employee_Location,Phone_Number,Vehicle_Id")] Employee employee)
        {
            if (ModelState.IsValid)
            {               
                _context.Add(employee);
            
               
               await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Vehicle_Id"] = new SelectList(_context.Set<Vehicle>(), "Vehicle_Id", "Vehicle_Id", employee.Vehicle_Id);
            
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["Vehicle_Id"] = new SelectList(_context.Set<Vehicle>(), "Vehicle_Id", "Vehicle_Id", employee.Vehicle_Id);
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Employee_Id,Employee_Name,Age,Employee_Location,Phone_Number,Vehicle_Id")] Employee employee)
        {
            if (id != employee.Employee_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Employee_Id))
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
            ViewData["Vehicle_Id"] = new SelectList(_context.Set<Vehicle>(), "Vehicle_Id", "Vehicle_Id", employee.Vehicle_Id);
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Vehicle)
                .FirstOrDefaultAsync(m => m.Employee_Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'FinalProject1Context.Employee'  is null.");
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return _context.Employee.Any(e => e.Employee_Id == id);
        }
        
    }
}
