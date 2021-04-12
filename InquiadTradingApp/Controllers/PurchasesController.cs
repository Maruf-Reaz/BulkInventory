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
    public class PurchasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Purchases
                .Include(p => p.Vendor)
                .Include(p => p.WareHouse)
                .Include(p => p.Product.ProductCatagory)
                .Include(p => p.PackSize)
                .Include(p => p.Product)
                .Include(p => p.Payments)
                .OrderByDescending(m => m.PurchaseDate);

            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            ViewData["ProductCatagoryId"] = new SelectList(_context.ProductCatagories, "Id", "Name");
            ViewData["WareHouseId"] = new SelectList(_context.WareHouses, "Id", "Name");
            ViewData["VendorId"] = new SelectList(_context.Vendors, "Id", "Name");
            ViewData["PackSizeId"] = new SelectList(_context.PackSizes, "Id", "Name");

            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> PurchaseStatement(DateTime fromDate, DateTime toDate)
        {
            if (fromDate == default(DateTime))
            {
                fromDate = DateTime.Now.AddDays(-30);
            }
            if (toDate == default(DateTime))
            {
                toDate = DateTime.Now.Date;
            }

            var purchases = await _context.Purchases
              .Include(p => p.Vendor)
                .Include(p => p.WareHouse)
                .Include(p => p.Product.ProductCatagory)
                .Include(p => p.PackSize)
                .Include(p => p.Product)
                .OrderByDescending(m => m.PurchaseDate)
             .Where(m => m.PurchaseDate >= fromDate && m.PurchaseDate <= toDate && m.Status == 1)
             .ToListAsync();


            ViewData["Purchases"] = purchases;
            ViewData["FromDate"] = fromDate.Date;
            ViewData["ToDate"] = toDate.Date;
            return View();
        }
        [HttpPost]
        public IActionResult PurchaseStatement(FromDateToDateViewModel datesVM)
        {

            return RedirectToAction("PurchaseStatement", new { id = datesVM.Id, fromDate = datesVM.FromDate, toDate = datesVM.ToDate });

        }
        public async Task<IActionResult> MISReport(DateTime fromDate, DateTime toDate)
        {
            if (fromDate == default(DateTime))
            {
                fromDate = DateTime.Now.AddDays(-30);
            }
            if (toDate == default(DateTime))
            {
                toDate = DateTime.Now.Date;
            }

            var purchases = await _context.Purchases
              .Include(p => p.Vendor)
                .Include(p => p.WareHouse)
                .Include(p => p.Product.ProductCatagory)
                .Include(p => p.PackSize)
                .Include(p => p.Product)
                .OrderByDescending(m => m.PurchaseDate)
             .Where(m => m.PurchaseDate >= fromDate && m.PurchaseDate <= toDate && m.Status == 1)
             .ToListAsync();

            StockMethod stockMethod = new StockMethod(_context);

            var misVM = stockMethod.GetMisReport(purchases);


            ViewData["misVM"] = misVM;
            ViewData["FromDate"] = fromDate.Date;
            ViewData["ToDate"] = toDate.Date;
            return View();
        }
        [HttpPost]
        public IActionResult MISReport(FromDateToDateViewModel datesVM)
        {

            return RedirectToAction("MISReport", new { id = datesVM.Id, fromDate = datesVM.FromDate, toDate = datesVM.ToDate });

        }
        public async Task<IActionResult> MISReportPercent(DateTime fromDate, DateTime toDate)
        {
            if (fromDate == default(DateTime))
            {
                fromDate = DateTime.Now.AddDays(-30);
            }
            if (toDate == default(DateTime))
            {
                toDate = DateTime.Now.Date;
            }

            var purchases = await _context.Purchases
              .Include(p => p.Vendor)
                .Include(p => p.WareHouse)
                .Include(p => p.Product.ProductCatagory)
                .Include(p => p.PackSize)
                .Include(p => p.Product)
                .OrderByDescending(m => m.PurchaseDate)
             .Where(m => m.PurchaseDate >= fromDate && m.PurchaseDate <= toDate && m.Status == 1)
             .ToListAsync();

            StockMethod stockMethod = new StockMethod(_context);

            var misVM = stockMethod.GetMISReportPercent(purchases);


            ViewData["misVM"] = misVM;
            ViewData["FromDate"] = fromDate.Date;
            ViewData["ToDate"] = toDate.Date;
            return View();
        }
        [HttpPost]
        public IActionResult MISReportPercent(FromDateToDateViewModel datesVM)
        {

            return RedirectToAction("MISReportPercent", new { id = datesVM.Id, fromDate = datesVM.FromDate, toDate = datesVM.ToDate });

        }


        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                 .Include(p => p.PackSize)
                .Include(p => p.Product)
                .Include(p => p.Product.ProductCatagory)
                .Include(p => p.Vendor)
                .Include(p => p.WareHouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        [HttpPost]
        public async Task<JsonResult> SavePurchase(DateTime purchaseDate, int vendorId, int productId, int warehouseId, string vendorWareHouse, double packAmount, int packSizeId, double tonAmount, double totalAmount, double paidAmount, int paymentMethod, string chequeNo, DateTime paymentDate, string remarks)
        {
            Purchase purchase = new Purchase
            {
                PurchaseDate = purchaseDate,
                VendorId = vendorId,
                ProductId = productId,
                WareHouseId = warehouseId,
                VendorWareWhouse = vendorWareHouse,
                PackAmount = packAmount,
                PackSizeId = packSizeId,
                TonAmount = tonAmount,
                TotalAmount = totalAmount,
                Remarks = remarks
            };
            int count = _context.Purchases.Count() + 1;
            purchase.VoucharNo = "PV000" + count.ToString();
            _context.Add(purchase);
            int result = await _context.SaveChangesAsync();

            if (result == 1)
            {
                Payment payment = new Payment
                {
                    Details = "Initial Payment",
                    Type = paymentMethod,
                    CheckNo = chequeNo,
                    Date = paymentDate,
                    PaidAmount = paidAmount,
                    PurchaseId= purchase.Id

                };
                _context.Add(payment);
                int result2 = await _context.SaveChangesAsync();

                if (result2==1)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }

            else
            {
                return Json(false);
            }
        }



        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                  .Include(p => p.PackSize)
                .Include(p => p.Product)
                .Include(p => p.Product.ProductCatagory)
                .Include(p => p.Vendor)
                .Include(p => p.WareHouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }
            if (purchase.Status != 0)
            {
                return NotFound();

            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
            return _context.Purchases.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ApprovePurchase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }
            else if (purchase.Status != 0)
            {
                return NotFound();
            }
            else
            {
                purchase.Status = 1;

                _context.Update(purchase);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        public async Task<JsonResult> GetPurchaseById(int? id)
        {
            if (id == null)
            {
                return Json(false);
            }
            var purchase = await _context.Purchases
               .Include(p => p.PackSize)
               .Include(m => m.Product)
               .Where(m => m.Id == id)
               .FirstOrDefaultAsync();



            if (purchase == null)
            {
                return Json(false);
            }
            return Json(purchase);
        }
        public ActionResult GetPurchaseDetails(int purchaseId)
        {
            var allPurchaseReturns = _context.PurchaseReturns.Where(m => m.PurchaseId == purchaseId).ToList();
            var allSales = _context.Sales.Include(m => m.Client).Where(m => m.PurchaseId == purchaseId).ToList();
            var allSaleReturns = _context.SaleReturns.Where(m => m.Sale.PurchaseId == purchaseId).ToList();
            var allLocalSales = _context.LocalSales.Where(m => m.PurchaseId == purchaseId).ToList();
            var allDamageProducts = _context.DamageProducts.Where(m => m.PurchaseId == purchaseId).ToList();
            var purchase = _context.Purchases
                .Include(m=>m.Product)
                .Include(m=>m.Vendor)
                .Include(m=>m.PackSize)
                .Where(m => m.Id == purchaseId).FirstOrDefault();
            ViewData["Purchase"] = purchase;
            ViewData["PurchaseReturns"] = allPurchaseReturns;
            ViewData["Sales"] = allSales;
            ViewData["SaleReturns"] = allSaleReturns;
            ViewData["LocalSales"] = allLocalSales;
            ViewData["DamagedProducts"] = allDamageProducts;
            return View();
        }


    }
}
