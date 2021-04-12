using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InquiadTradingApp.Data;
using InquiadTradingApp.Models;
using InquiadTradingApp.Models.ViewModels;

namespace InquiadTradingApp.Controllers
{
    public class LocalSalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocalSalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LocalSales
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LocalSales
                .Include(l => l.Purchase)
                .Include(d => d.Purchase.Product)
                .Include(d => d.Purchase.PackSize)
                .Include(d => d.Purchase.WareHouse);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: LocalSales/Create
        public IActionResult Create()
        {
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "Id", "VoucharNo");
            return View();
        }

        // POST: LocalSales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocalSale localSale)
        {
            int count = _context.LocalSales.Count()+1;
            localSale.VoucharNo = "LSV000" + count.ToString();

            var purchase =await _context.Purchases.Where(m => m.Id == localSale.PurchaseId).FirstOrDefaultAsync();
            var packSize = await _context.PackSizes.Where(m => m.Id == purchase.PackSizeId).FirstOrDefaultAsync();

            localSale.TonAMount = (localSale.PackAmount*packSize.TotalKilo) / 1000;
            
            localSale.Status = 0;


            if (ModelState.IsValid)
            {
                _context.Add(localSale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "Id", "VoucharNo", localSale.PurchaseId);
            return View(localSale);
        }

        public async Task<IActionResult> LocalSaleStatement(DateTime fromDate, DateTime toDate)
        {
            if (fromDate == default(DateTime))
            {
                fromDate = DateTime.Now.AddDays(-30);
            }
            if (toDate == default(DateTime))
            {
                toDate = DateTime.Now.Date;
            }

            var sales = await _context.LocalSales
                .Include(s => s.Purchase.PackSize)
                .Include(s => s.Purchase.Product)
                .Include(s => s.Purchase.WareHouse)
                .Include(s => s.Purchase.Product.ProductCatagory)
                 .OrderByDescending(m => m.SaleDate)
             .Where(m => m.SaleDate >= fromDate && m.SaleDate <= toDate && m.Status == 1)
             .ToListAsync();

            ViewData["LocalSales"] = sales;
            ViewData["FromDate"] = fromDate.Date;
            ViewData["ToDate"] = toDate.Date;
            return View();
        }
        [HttpPost]
        public IActionResult LocalSaleStatement(FromDateToDateViewModel datesVM)
        {

            return RedirectToAction("LocalSaleStatement", new { id = datesVM.Id, fromDate = datesVM.FromDate, toDate = datesVM.ToDate });

        }
        // GET: LocalSales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localSale = await _context.LocalSales
                .Include(l => l.Purchase)
                .Include(d => d.Purchase)
                .Include(d => d.Purchase.Product)
                .Include(d => d.Purchase.WareHouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (localSale == null)
            {
                return NotFound();
            }

            return View(localSale);
        }

        // POST: LocalSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var localSale = await _context.LocalSales.FindAsync(id);
            _context.LocalSales.Remove(localSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalSaleExists(int id)
        {
            return _context.LocalSales.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ApproveLocalSale(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localSale = await _context.LocalSales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (localSale == null)
            {
                return NotFound();
            }
            else if (localSale.Status != 0)
            {
                return NotFound();
            }
            else
            {
                localSale.Status = 1;

                _context.Update(localSale);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

        }


    }
}
