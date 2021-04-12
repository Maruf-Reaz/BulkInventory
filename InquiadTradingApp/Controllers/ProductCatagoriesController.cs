using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InquiadTradingApp.Data;
using InquiadTradingApp.Models;

namespace InquiadTradingApp.Controllers
{
    public class ProductCatagoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductCatagoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductCatagories
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductCatagories.ToListAsync());
        }

        // GET: ProductCatagories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCatagory = await _context.ProductCatagories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productCatagory == null)
            {
                return NotFound();
            }

            return View(productCatagory);
        }

        // GET: ProductCatagories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductCatagories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ProductCatagory productCatagory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productCatagory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productCatagory);
        }

        // GET: ProductCatagories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCatagory = await _context.ProductCatagories.FindAsync(id);
            if (productCatagory == null)
            {
                return NotFound();
            }
            return View(productCatagory);
        }

        // POST: ProductCatagories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ProductCatagory productCatagory)
        {
            if (id != productCatagory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCatagory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCatagoryExists(productCatagory.Id))
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
            return View(productCatagory);
        }

        // GET: ProductCatagories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCatagory = await _context.ProductCatagories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productCatagory == null)
            {
                return NotFound();
            }

            return View(productCatagory);
        }

        // POST: ProductCatagories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productCatagory = await _context.ProductCatagories.FindAsync(id);
            _context.ProductCatagories.Remove(productCatagory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCatagoryExists(int id)
        {
            return _context.ProductCatagories.Any(e => e.Id == id);
        }
    }
}
