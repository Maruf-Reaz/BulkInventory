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
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.ProductCatagory);
            return View(await applicationDbContext.ToListAsync());
        }

         public async Task<IActionResult> ProductIndex()
        {
            var applicationDbContext = _context.Products.Include(p => p.ProductCatagory);
            return View(await applicationDbContext.ToListAsync());
        }




        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCatagory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProductCatagoryId"] = new SelectList(_context.ProductCatagories, "Id", "Name");
            ViewData["PackSizeId"] = new SelectList(_context.PackSizes, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
           

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductCatagoryId"] = new SelectList(_context.ProductCatagories, "Id", "Name", product.ProductCatagoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductCatagoryId"] = new SelectList(_context.ProductCatagories, "Id", "Name", product.ProductCatagoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["ProductCatagoryId"] = new SelectList(_context.ProductCatagories, "Id", "Name", product.ProductCatagoryId);
           
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCatagory)
              
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<JsonResult> GetProductByCatagory(int? id)
        {
            if (id == null)
            {
                return Json(false);
            }
            var products = await _context.Products
               .Where(m => m.ProductCatagoryId == id)
               .ToListAsync();



            if (products == null)
            {
                return Json(false);
            }
            return Json(products);
        }


        [HttpPost]
        public JsonResult GetPurchaseProductById(int id)
        {
            if (id == 0)
            {
                return Json(false);
            }

            StockMethod stockMethod = new StockMethod(_context);

            var purchaseVM = stockMethod.GetPurchaseStock(id);


            if (purchaseVM == null)
            {
                return Json(false);
            }
            return Json(purchaseVM);
        }


         [HttpPost]
        public JsonResult GetPurchaseForTransferById(int id)
        {
            if (id == 0)
            {
                return Json(false);
            }

            StockMethod stockMethod = new StockMethod(_context);


            var purchaseVM = stockMethod.GetPurchaseStock(id);


            if (purchaseVM == null)
            {
                return Json(false);
            }
            return Json(purchaseVM);
        }





        [HttpPost]
        public async Task<JsonResult> GetProductById(int? id)
        {
            if (id == null)
            {
                return Json(false);
            }
            var product = await _context.Products
               
               .Where(m => m.Id == id)
               .FirstOrDefaultAsync();



            if (product == null)
            {
                return Json(false);
            }
            return Json(product);
        }
        public ActionResult StockIndex(int id)
        {
            StockMethod stockMethod = new StockMethod(_context);

            List<StockViewModel> stockViewModels = stockMethod.GetProductStock(id);

            var product = _context.Products.Where(m=>m.Id==id).FirstOrDefault();

            ViewData["StockItems"] = stockViewModels;
            ViewData["Product"] = product;

            return View();
        }


        public ActionResult ShowDetails(int id)
        {
            //var product = _context.Products
            //   .Include(i => i.PackSize)
            //   .Include(i => i.ProductCatagory)
            //   .Where(m => m.Id == id)
            //   .FirstOrDefault();

            //var warehouses = _context.WareHouses
            //  .ToList();


            //List<StockViewModel> stockViewModels = new List<StockViewModel>();

            //foreach (var warehouse in warehouses)
            //{
            //    StockViewModel stockViewModel = new StockViewModel();
            //    var allPurchases = _context.Purchases
            //        .Where(m => m.ProductId == product.Id && m.WareHouseId == warehouse.Id && m.Status == 1)
            //        .ToList();

            //    var allPurchaseReturns = _context.PurchaseReturns
            //      .Where(m => m.Purchase.ProductId == product.Id && m.Purchase.WareHouseId == warehouse.Id && m.Status == 1)
            //      .ToList();

            //    var allTransferReceives = _context.WareHouseTransfers
            //       .Where(m => m.Purchase.ProductId == product.Id && m.ToWareHouseId == warehouse.Id && m.Status == 1)
            //       .ToList();

            //    var allTransferSends = _context.WareHouseTransfers
            //     .Where(m => m.Purchase.ProductId == product.Id && m.Purchase.WareHouseId == warehouse.Id && m.Status == 1)
            //     .ToList();



            //    var allSales = _context.Sales
            //       .Where(m => m.Purchase.ProductId == product.Id && m.Purchase.WareHouseId == warehouse.Id && m.Status == 1)
            //       .ToList();
            //    var allSaleReturns = _context.SaleReturns
            //       .Where(m => m.Sale.Purchase.ProductId == product.Id && m.Sale.Purchase.WareHouseId == warehouse.Id && m.Status == 1)
            //       .ToList();


            //    double totalTransferReceivePack = (double)allTransferReceives.Sum(m => m.TransferAmountPackSize);
            //    double totalTransferReceiveTon = (double)allTransferReceives.Sum(m => m.TransferAmountInTons);

            //    double totalTransferSendPack = (double)allTransferSends.Sum(m => m.TransferAmountPackSize);
            //    double totalTransferSendTon = (double)allTransferSends.Sum(m => m.TransferAmountInTons);



            //    double totalPurchasePack = (double)allPurchases.Sum(m => m.PackAmount);
            //    double totalPurchaseTon = (double)allPurchases.Sum(m => m.TonAmount);





            //    double totalPurchaseReturnPack = (double)allPurchaseReturns.Sum(m => m.PackAmount);
            //    double totalPurchaseReturnTon = (double)allPurchaseReturns.Sum(m => m.TonAmount);

            //    double totalSalePack = (double)allSales.Sum(m => m.TonAmount);
            //    double totalSaleTon = (double)allSales.Sum(m => m.TonAmount);

            //    double totalSaleReturnPack = (double)allSaleReturns.Sum(m => m.PackAmount);
            //    double totalSaleReturnTon = (double)allSaleReturns.Sum(m => m.TonAmount);

            //    double currentStockPack = (totalPurchasePack - totalPurchaseReturnPack) - (totalSalePack - totalSaleReturnPack)+(totalTransferReceivePack-totalTransferSendPack);
            //    double currentStockTon = (totalPurchaseTon - totalPurchaseReturnTon) - (totalSaleTon - totalSaleReturnTon) + (totalTransferReceiveTon - totalTransferSendTon);


            //    stockViewModel.Product = product;
            //    stockViewModel.ProductId = product.Id;
            //      stockViewModel.WareHouse = warehouse;
            //    stockViewModel.WareHouseId = warehouse.Id;

            //    stockViewModel.CurrentStockPack = currentStockPack;
            //    stockViewModel.CurrentStockTon = currentStockTon;
            //    stockViewModels.Add(stockViewModel);
            //}
            //ViewData["StockItems"] = stockViewModels;
            //ViewData["Product"] = product;

            return View();
        }




    }
}
