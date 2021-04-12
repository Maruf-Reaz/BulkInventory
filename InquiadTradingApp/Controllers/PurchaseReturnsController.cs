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
    public class PurchaseReturnsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseReturnsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseReturns
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PurchaseReturns
                .Include(p => p.Purchase)
                .Include(p => p.Purchase.Product)
                .Include(p => p.Purchase.PackSize)
                 .OrderByDescending(m => m.ReturnDate);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> PurchaseReturnStatement(DateTime fromDate, DateTime toDate)
        {
            if (fromDate == default(DateTime))
            {
                fromDate = DateTime.Now.AddDays(-30);
            }
            if (toDate == default(DateTime))
            {
                toDate = DateTime.Now.Date;
            }

            var purchaseReturns = await _context.PurchaseReturns
              .Include(p => p.Purchase)
                .Include(p => p.Purchase.Product)
                .Include(p => p.Purchase.PackSize)
                 .OrderByDescending(m => m.ReturnDate)
             .Where(m => m.ReturnDate >= fromDate && m.ReturnDate <= toDate && m.Status==1)
             .ToListAsync();

            ViewData["PurchaseReturns"] = purchaseReturns;
            ViewData["FromDate"] = fromDate.Date;
            ViewData["ToDate"] = toDate.Date;
            return View();
        }
        [HttpPost]
        public IActionResult PurchaseReturnStatement(FromDateToDateViewModel datesVM)
        {

            return RedirectToAction("PurchaseReturnStatement", new { id = datesVM.Id, fromDate = datesVM.FromDate, toDate = datesVM.ToDate });

        }

        // GET: PurchaseReturns/Create
        public IActionResult Create(int purchaseId)
        {
            var purchase = _context.Purchases
                .Include(m=>m.Product)
                .Include(m=>m.PackSize)
                .Where(m => m.Id == purchaseId).FirstOrDefault();
            StockMethod stockMethod = new StockMethod(_context);
            var purchaseStockDetails = stockMethod.GetPurchaseStock(purchaseId);
            ViewData["Purchase"] = purchase;
            ViewData["StockDetails"] = purchaseStockDetails;
            return View();
        }

        // POST: PurchaseReturns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseReturn purchaseReturn)
        {
            int count = _context.PurchaseReturns.Count()+1;
            purchaseReturn.VoucharNo = "PRV000" + count.ToString();
            purchaseReturn.Status = 0;
            if (ModelState.IsValid)
            {
                _context.Add(purchaseReturn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var purchase = _context.Purchases
               .Include(m => m.Product)
               .Include(m => m.PackSize)
               .Where(m => m.Id == purchaseReturn.PurchaseId).FirstOrDefault();
            StockMethod stockMethod = new StockMethod(_context);
            var purchaseStockDetails = stockMethod.GetPurchaseStock(purchaseReturn.PurchaseId);
            ViewData["Purchase"] = purchase;
            ViewData["StockDetails"] = purchaseStockDetails;
            return View(purchaseReturn);
        }


        // GET: PurchaseReturns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseReturn = await _context.PurchaseReturns
                .Include(p => p.Purchase)
                .Include(p => p.Purchase.Product)
                .Include(p => p.Purchase.PackSize)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseReturn == null)
            {
                return NotFound();
            }

            return View(purchaseReturn);
        }

        // POST: PurchaseReturns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseReturn = await _context.PurchaseReturns.FindAsync(id);
            _context.PurchaseReturns.Remove(purchaseReturn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseReturnExists(int id)
        {
            return _context.PurchaseReturns.Any(e => e.Id == id);
        }


        public async Task<IActionResult> ApprovePurchaseReturn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseReturn = await _context.PurchaseReturns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseReturn == null)
            {
                return NotFound();
            }
            else if (purchaseReturn.Status != 0)
            {
                return NotFound();
            }
            else
            {
                purchaseReturn.Status = 1;

                _context.Update(purchaseReturn);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

        }

    }
}
