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
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> PurchaseIndex(int purchaseId)
        {
            var applicationDbContext = _context.Payments.Where(m=>m.PurchaseId==purchaseId).Include(p => p.Purchase).Include(p => p.Sale);

            double totalPaid = applicationDbContext.Sum(m => m.PaidAmount);
            var purchase = await _context.Purchases.Where(m => m.Id == purchaseId).FirstOrDefaultAsync();
            ViewData["TotalPaid"] = totalPaid;
            ViewData["Purchase"] = purchase;

            ViewData["PurchaseId"] = purchaseId;
            return View(await applicationDbContext.ToListAsync());
        }
         public async Task<IActionResult> SaleIndex(int saleId)
        {
            var applicationDbContext = _context.Payments.Where(m=>m.SaleId== saleId).Include(p => p.Purchase).Include(p => p.Sale);
            double totalPaid = applicationDbContext.Sum(m => m.PaidAmount);
            var sale = await _context.Sales.Where(m => m.Id == saleId).FirstOrDefaultAsync();
            ViewData["TotalPaid"] = totalPaid;
            ViewData["Sale"] = sale;
            ViewData["SaleId"] = saleId;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Purchase)
                .Include(p => p.Sale)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult PurchaseCreate(int purchaseId)
        {
            ViewData["PurchaseId"] = purchaseId;
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PurchaseCreate(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction("PurchaseIndex", new { purchaseId = payment.PurchaseId });
            }
          
            return View(payment);
        }


          // GET: Payments/Create
        public IActionResult SaleCreate(int saleId)
        {
            ViewData["SaleId"] = saleId;
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaleCreate(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction("SaleIndex", new { saleId = payment.SaleId});
                
            }
          
            return View(payment);
        }




        
        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? paymentId)
        {
            if (paymentId == null)
            {
                return NotFound();
            }
            var payment = await _context.Payments.FindAsync(paymentId);

            bool IsSale= false;
            int purchaseOrSaleId = 0;
            if (payment.SaleId==null)
            {
                IsSale = false;
                purchaseOrSaleId =(int) payment.PurchaseId;
            }
            else if (payment.PurchaseId==null)
            {
                IsSale = true;
                purchaseOrSaleId = (int)payment.SaleId;
            }
          
            if (payment == null)
            {
                return NotFound();
            }
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            if (purchaseOrSaleId==0)
            {
                return NotFound();
            }
            if (IsSale)
            {
                return RedirectToAction("SaleIndex", new { saleId = purchaseOrSaleId });
            }
            else
            {
                return RedirectToAction("PurchaseIndex", new { purchaseId = purchaseOrSaleId });
            }
           
        }
        
    }
}
