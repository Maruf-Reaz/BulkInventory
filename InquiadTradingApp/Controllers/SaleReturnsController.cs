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
    public class SaleReturnsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SaleReturnsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SaleReturns
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SaleReturns
                .Include(p => p.Sale)
                .Include(p => p.Sale.Purchase)
                .Include(p => p.Sale.Purchase.Product)
                .Include(p => p.Sale.Purchase.PackSize).OrderByDescending(m => m.ReturnDate);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> SalesReturnStatement(DateTime fromDate, DateTime toDate)
        {
            if (fromDate == default(DateTime))
            {
                fromDate = DateTime.Now.AddDays(-30);
            }
            if (toDate == default(DateTime))
            {
                toDate = DateTime.Now.Date;
            }

            var saleReturns = await _context.SaleReturns
             .Include(p => p.Sale)
                .Include(p => p.Sale.Purchase)
                .Include(p => p.Sale.Purchase.Product)
                .Include(p => p.Sale.Purchase.PackSize).OrderByDescending(m => m.ReturnDate)
             .Where(m => m.ReturnDate >= fromDate && m.ReturnDate <= toDate && m.Status == 1)
             .ToListAsync();

            ViewData["SaleReturns"] = saleReturns;
            ViewData["FromDate"] = fromDate.Date;
            ViewData["ToDate"] = toDate.Date;
            return View();
        }
        [HttpPost]
        public IActionResult SalesReturnStatement(FromDateToDateViewModel datesVM)
        {

            return RedirectToAction("SalesReturnStatement", new { id = datesVM.Id, fromDate = datesVM.FromDate, toDate = datesVM.ToDate });

        }


        // GET: SaleReturns/Details/5

        // GET: SaleReturns/Create
        public IActionResult Create(int saleId)
        {
            var sale = _context.Sales
              .Include(m => m.Purchase)
              .Include(m => m.Purchase.Product)
              .Include(m => m.Purchase.PackSize)
              .Where(m => m.Id == saleId).FirstOrDefault();

            ViewData["Sale"] = sale;
            return View();
        }

        // POST: SaleReturns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleReturn saleReturn)
        {
            int count = _context.SaleReturns.Count() + 1;
            saleReturn.VoucharNo = "SRV000" + count.ToString();
            saleReturn.Status = 0;

            if (ModelState.IsValid)
            {
                _context.Add(saleReturn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var sale = _context.Sales
              .Include(m => m.Purchase)
              .Include(m => m.Purchase.Product)
              .Include(m => m.Purchase.PackSize)
              .Where(m => m.Id == saleReturn.SaleId).FirstOrDefault();

            ViewData["Sale"] = sale;
            return View(saleReturn);
        }

        // GET: SaleReturns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleReturn = await _context.SaleReturns
                .Include(s => s.Sale)
                .Include(p => p.Sale.Purchase)
                .Include(p => p.Sale.Purchase.Product)
                .Include(p => p.Sale.Purchase.PackSize)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleReturn == null)
            {
                return NotFound();
            }

            return View(saleReturn);
        }

        // POST: SaleReturns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleReturn = await _context.SaleReturns.FindAsync(id);
            _context.SaleReturns.Remove(saleReturn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleReturnExists(int id)
        {
            return _context.SaleReturns.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ApproveSaleReturn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SaleReturn = await _context.SaleReturns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (SaleReturn == null)
            {
                return NotFound();
            }
            else if (SaleReturn.Status != 0)
            {
                return NotFound();
            }
            else
            {
                SaleReturn.Status = 1;

                _context.Update(SaleReturn);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

        }


    }
}
