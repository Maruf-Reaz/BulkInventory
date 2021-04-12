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
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sales
                .Include(s => s.Broker)
                .Include(s => s.Purchase.Product)
                .Include(s => s.Purchase.WareHouse)
                .Include(s => s.Purchase.PackSize)
                .Include(s => s.Purchase)
                .Include(s => s.Client)
                .Include(s => s.Payments)
                .Include(s => s.Purchase.Product.ProductCatagory)
                 .OrderByDescending(m => m.SaleDate);

            var purchases = await _context.Purchases.Where(m => m.Status == 1).ToListAsync();

            List<Purchase> finalPurchases = new List<Purchase>();
            foreach (var purchase in purchases)
            {
                StockMethod stockMethod = new StockMethod(_context);
                StockViewModel stockVM =  stockMethod.GetPurchaseStock(purchase.Id);
                if (stockVM.CurrentStockPack!=0 && stockVM.CurrentStockTon!=0)
                {
                    finalPurchases.Add(purchase);
                }
            }


            ViewData["BrokerId"] = new SelectList(_context.Brokers, "Id", "Name");
            ViewData["PurchaseId"] = new SelectList(finalPurchases, "Id", "VoucharNo");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            ViewData["WareHouseId"] = new SelectList(_context.WareHouses, "Id", "Name");
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name");
            ViewData["ProductCatagoryId"] = new SelectList(_context.ProductCatagories, "Id", "Name");
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> SaleStatement(DateTime fromDate, DateTime toDate)
        {
            if (fromDate == default(DateTime))
            {
                fromDate = DateTime.Now.AddDays(-30);
            }
            if (toDate == default(DateTime))
            {
                toDate = DateTime.Now.Date;
            }

            var sales = await _context.Sales
             .Include(s => s.Broker)
                .Include(s => s.Purchase.PackSize)
                .Include(s => s.Purchase.Product)
                .Include(s => s.Purchase.WareHouse)
                .Include(s => s.Client)
                .Include(s => s.Purchase.Product.ProductCatagory)
                 .OrderByDescending(m => m.SaleDate)
             .Where(m => m.SaleDate >= fromDate && m.SaleDate <= toDate && m.Status == 1)
             .ToListAsync();

            ViewData["Sales"] = sales;
            ViewData["FromDate"] = fromDate.Date;
            ViewData["ToDate"] = toDate.Date;
            return View();
        }
        [HttpPost]
        public IActionResult SaleStatement(FromDateToDateViewModel datesVM)
        {

            return RedirectToAction("SaleStatement", new { id = datesVM.Id, fromDate = datesVM.FromDate, toDate = datesVM.ToDate });

        }


        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Purchase)
                .Include(s => s.Broker)
                .Include(s => s.Purchase.PackSize)
                .Include(s => s.Purchase.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        [HttpPost]
        public async Task<JsonResult> SaveSale(int purchaseId, DateTime saleDate, int clientId, int brokerId, double packAmount, double tonAmount, double totalAmount,double paidAmount, double brokerAmount, string clientWarehouse, int paymentMethod, string chequeNo, DateTime paymentDate, string remarks)
        {
            Sale sale = new Sale
            {
                PurchaseId = purchaseId,
                SaleDate = saleDate,
                ClientId = clientId,
                BrokerId = brokerId,
                PackAmount = packAmount,
                TonAmount = tonAmount,
                TotalAmount = totalAmount,
                BrokerAMount = brokerAmount,
                ClientWareHouse = clientWarehouse,
                Remarks = remarks
            };
            int count = _context.Sales.Count() + 1;
            sale.VoucharNo = "SV000" + count.ToString();
            _context.Add(sale);
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
                    SaleId = sale.Id

                };
                _context.Add(payment);
                int result2 = await _context.SaveChangesAsync();

                if (result2 == 1)
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



        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Broker)
                .Include(s => s.Purchase.PackSize)
                .Include(s => s.Purchase)
                .Include(s => s.Purchase.Product)
                .Include(s => s.Client)
                .Include(s => s.Purchase.WareHouse)
                .Include(s => s.Purchase.Product.ProductCatagory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ApproveSale(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }
            else if (sale.Status != 0)
            {
                return NotFound();
            }
            else
            {
                sale.Status = 1;

                _context.Update(sale);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        public async Task<JsonResult> GetSaleById(int? id)
        {
            if (id == null)
            {
                return Json(false);
            }
            var sale = await _context.Sales
                .Include(s => s.Purchase.PackSize)
                .Include(m => m.Purchase)
                .Include(m => m.Purchase.Product)
               .Where(m => m.Id == id)
               .FirstOrDefaultAsync();



            if (sale == null)
            {
                return Json(false);
            }
            return Json(sale);
        }



    }
}
