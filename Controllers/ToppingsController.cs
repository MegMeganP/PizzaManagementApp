using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaApplication.Data;
using PizzaApplication.Models;

namespace PizzaApplication.Controllers
{
    public class ToppingsController : Controller
    {
        private readonly DataContext _context;

        public ToppingsController(DataContext context)
        {
            _context = context;
        }

        // GET: Toppings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Toppings.ToListAsync());
        }

        // GET: Toppings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topping = await _context.Toppings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topping == null)
            {
                return NotFound();
            }

            return View(topping);
        }

        // GET: Toppings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Toppings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ToppingName")] Topping topping)
        {
            var duplicateTopping = _context.Toppings.Where(p => p.ToppingName == topping.ToppingName).Count();

            if (duplicateTopping > 0)
            {
                ModelState.AddModelError("ToppingName", "Can't have duplicate Toppings");
            }
            if (ModelState.IsValid)
            {
                _context.Add(topping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topping);
        }

        // GET: Toppings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topping = await _context.Toppings.FindAsync(id);
            if (topping == null)
            {
                return NotFound();
            }
            return View(topping);
        }

        // POST: Toppings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ToppingName")] Topping topping)
        {
            if (id != topping.Id)
            {
                return NotFound();
            }

            var duplicateTopping = _context.Toppings.Where(p => p.ToppingName == topping.ToppingName).Count();

            if (duplicateTopping > 0)
            {
                ModelState.AddModelError("ToppingName", "Can't have duplicate Toppings");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToppingExists(topping.Id))
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
            return View(topping);
        }

        // GET: Toppings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topping = await _context.Toppings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topping == null)
            {
                return NotFound();
            }

            return View(topping);
        }

        // POST: Toppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var topping = await _context.Toppings.FindAsync(id);
            _context.Toppings.Remove(topping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToppingExists(long id)
        {
            return _context.Toppings.Any(e => e.Id == id);
        }
    }
}
