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
    public class PackSizesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PackSizesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PackSizes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PackSizes.ToListAsync());
        }

        // GET: PackSizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packSize = await _context.PackSizes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (packSize == null)
            {
                return NotFound();
            }

            return View(packSize);
        }

        // GET: PackSizes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PackSizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TotalKilo")] PackSize packSize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(packSize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(packSize);
        }

        // GET: PackSizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packSize = await _context.PackSizes.FindAsync(id);
            if (packSize == null)
            {
                return NotFound();
            }
            return View(packSize);
        }

        // POST: PackSizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TotalKilo")] PackSize packSize)
        {
            if (id != packSize.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(packSize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackSizeExists(packSize.Id))
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
            return View(packSize);
        }

        // GET: PackSizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packSize = await _context.PackSizes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (packSize == null)
            {
                return NotFound();
            }

            return View(packSize);
        }

        // POST: PackSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var packSize = await _context.PackSizes.FindAsync(id);
            _context.PackSizes.Remove(packSize);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackSizeExists(int id)
        {
            return _context.PackSizes.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<JsonResult> GetPackSizeById(int? id)
        {
            if (id == null)
            {
                return Json(false);
            }
            var packSize = await _context.PackSizes
               .Where(m => m.Id== id)
               .FirstOrDefaultAsync();



            if (packSize == null)
            {
                return Json(false);
            }
            return Json(packSize);
        }



    }
}
