using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaApplication.Data;
using PizzaApplication.Migrations;
using PizzaApplication.Models;

namespace PizzaApplication.Controllers
{
    public class PizzasController : Controller
    {
        private readonly DataContext _context;

        public PizzasController(DataContext context)
        {
            _context = context;
        }

        // GET: Pizzas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pizzas.ToListAsync());
        }

        // GET: Pizzas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // GET: Pizzas/Create
        public IActionResult Create()
        {
            var pizza = new Pizza();

            var toppings = _context.Toppings.ToList();
            foreach(var topping in toppings)
            {
                pizza.SelectedToppings.Add(new SelectedTopping() { Id = topping.Id, Selected = false, ToppingName = topping.ToppingName });
            }
         
            return View(pizza);
        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SelectedToppings")] Pizza pizza)
        {
            var duplicateName = _context.Pizzas.Where(p => p.Name == pizza.Name).Count();

            if (duplicateName > 0)
            {
                ModelState.AddModelError("Name", "Can't have duplicate pizzas");
            }
            if (ModelState.IsValid)
            {
                foreach (var selectedTopping in pizza.SelectedToppings.Where(x=>x.Selected))
                {
                    pizza.PizzaToppings.Add(new PizzaTopping() { ToppingId = selectedTopping.Id });
                }
                _context.Add(pizza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pizza);
        }

        // GET: Pizzas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas.Include("PizzaToppings.Topping").SingleAsync(x => x.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            var toppings = _context.Toppings.ToList();
            foreach (var topping in toppings)
            {
                var selected = pizza.PizzaToppings.Any(x => x.ToppingId == topping.Id);
                pizza.SelectedToppings.Add(new SelectedTopping() { Id = topping.Id, Selected = selected, ToppingName = topping.ToppingName });
            }
            return View(pizza);
        }

        // POST: Pizzas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,SelectedToppings")] Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return NotFound();
            }

            var duplicateName = _context.Pizzas.Where(p => p.Name == pizza.Name).Count();

            if (duplicateName > 0)
            {
                ModelState.AddModelError("Name", "Can't have duplicate pizzas");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var pizzaDb = await _context.Pizzas.Include("PizzaToppings.Topping").SingleAsync(x => x.Id == id);
                    var pizzaDbSelectedToppingIds = pizzaDb.PizzaToppings.Select(x => x.ToppingId).ToList();
                    //add new toppins
                    foreach (var selectedTopping in pizza.SelectedToppings.Where(x => x.Selected && !pizzaDbSelectedToppingIds.Contains(x.Id)))
                    {
                        pizzaDb.PizzaToppings.Add(new PizzaTopping() { ToppingId = selectedTopping.Id });
                    }
                    //remove old toppings
                    foreach (var deletedTopping in pizza.SelectedToppings.Where(x => !x.Selected && pizzaDbSelectedToppingIds.Contains(x.Id)))
                    {
                        var deletedPizzaTopping = pizzaDb.PizzaToppings.Single(x => x.ToppingId == deletedTopping.Id);
                        pizzaDb.PizzaToppings.Remove(deletedPizzaTopping);
                        _context.Remove(deletedPizzaTopping);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaExists(pizza.Id))
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
            return View(pizza);
        }

        // GET: Pizzas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaExists(long id)
        {
            return _context.Pizzas.Any(e => e.Id == id);
        }
    }
}
