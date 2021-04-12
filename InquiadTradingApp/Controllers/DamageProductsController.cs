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
    public class DamageProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DamageProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DamageProducts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DamageProducts
                .Include(d => d.Purchase)
                .Include(d => d.Purchase.Product)
                .Include(d => d.Purchase.PackSize)
                .Include(d => d.Purchase.WareHouse);
            return View(await applicationDbContext.ToListAsync());
        }

      

        // GET: DamageProducts/Create
        public IActionResult Create()
        {
            ViewData["PurchaseId"] = new SelectList(_context.Purchases.Where(m=>m.Status==1), "Id", "VoucharNo");
            return View();
        }

        // POST: DamageProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( DamageProduct damageProduct)
        {
            int count = _context.DamageProducts.Count();
            damageProduct.VoucharNo = "DV000" + count.ToString();
            damageProduct.TonAMount = damageProduct.KGAmount / 1000;

            var purchase = await _context.Purchases
                .Include(m=>m.PackSize)
                .Where(m => m.Id == damageProduct.PurchaseId).FirstOrDefaultAsync();
            damageProduct.PackAmount = damageProduct.KGAmount / purchase.PackSize.TotalKilo;
            damageProduct.Status = 0;

            if (ModelState.IsValid)
            {
                _context.Add(damageProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PurchaseId"] = new SelectList(_context.Purchases.Where(m => m.Status == 1), "Id", "VoucharNo", damageProduct.PurchaseId);
            return View(damageProduct);
        }

        

        // GET: DamageProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var damageProduct = await _context.DamageProducts
                .Include(d => d.Purchase)
                .Include(d => d.Purchase.Product)
                .Include(d => d.Purchase.WareHouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (damageProduct == null)
            {
                return NotFound();
            }

            return View(damageProduct);
        }

        // POST: DamageProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var damageProduct = await _context.DamageProducts.FindAsync(id);
            _context.DamageProducts.Remove(damageProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DamageProductExists(int id)
        {
            return _context.DamageProducts.Any(e => e.Id == id);
        }


        public async Task<IActionResult> ApproveDamage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var damage = await _context.DamageProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (damage == null)
            {
                return NotFound();
            }
            else if (damage.Status != 0)
            {
                return NotFound();
            }
            else
            {
                damage.Status = 1;

                _context.Update(damage);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

        }
    }
}
