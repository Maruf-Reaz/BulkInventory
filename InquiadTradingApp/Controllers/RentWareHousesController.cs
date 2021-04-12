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
    public class RentWareHousesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentWareHousesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RentWareHouses
        public async Task<IActionResult> Index()
        {
            return View(await _context.RentWareHouses.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        // POST: RentWareHouses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentWareHouse rentWareHouse)
        {
            rentWareHouse.Status = 0;
            if (ModelState.IsValid)
            {
                _context.Add(rentWareHouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rentWareHouse);
        }

        // GET: RentWareHouses/Edit/5
        public async Task<IActionResult> RentItemIndex(int? rentWareHouseId)
        {
            if (rentWareHouseId == null)
            {
                return NotFound();
            }

            var rentWareHouse = await _context.RentWareHouses
                .Include(m => m.RentWareHouseItems)
                 .ThenInclude(m => m.Purchase)
                .Where(m => m.Id == rentWareHouseId)
                .FirstOrDefaultAsync();

            var purchases = await _context.Purchases.Where(m => m.Status == 1).ToListAsync();

            List<Purchase> finalPurchases = new List<Purchase>();
            foreach (var purchase in purchases)
            {
                StockMethod stockMethod = new StockMethod(_context);
                StockViewModel stockVM = stockMethod.GetPurchaseStock(purchase.Id);
                if (stockVM.CurrentStockPack != 0 && stockVM.CurrentStockTon != 0)
                {
                    finalPurchases.Add(purchase);
                }
            }
            ViewData["PurchaseId"] = finalPurchases;
            ViewData["RentWareHouseId"] = rentWareHouseId;


            if (rentWareHouse == null)
            {
                return NotFound();
            }
            return View(rentWareHouse);
        }
        // GET: RentWareHouses/Edit/5
        public async Task<IActionResult> RentHistory(int? rentWareHouseId)
        {
            if (rentWareHouseId == null)
            {
                return NotFound();
            }

            var rentWareHouse = await _context.RentWareHouses
                .Include(m => m.RentWareHouseItems)
                .ThenInclude(m => m.Purchase)
                .Where(m => m.Id == rentWareHouseId)
                .FirstOrDefaultAsync();
            


            if (rentWareHouse == null)
            {
                return NotFound();
            }
            return View(rentWareHouse);
        }

        [HttpPost]
        public async Task<JsonResult> SaveRentItem(int purchaseId, string description, DateTime fromDate, DateTime toDate, double amount, int rentWareHouseId)
        {
            RentWareHouseItem rentItem = new RentWareHouseItem
            {
                PurchaseId = purchaseId,
                FromDate = fromDate,
                ToDate = toDate,
                Details = description,
                Amount = amount,
                RentWareHouseId = rentWareHouseId,
            };
            _context.Add(rentItem);
            int result = await _context.SaveChangesAsync();


            if (result == 1)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }



        // GET: RentWareHouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentWareHouse = await _context.RentWareHouses.FindAsync(id);
            _context.RentWareHouses.Remove(rentWareHouse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         
        }
        public async Task<IActionResult> DeleteItem(int? rentItemId)
        {
            if (rentItemId == null)
            {
                return NotFound();
            }

            var rentWareHouseItem = await _context.RentWareHouseItems.FindAsync(rentItemId);
            _context.RentWareHouseItems.Remove(rentWareHouseItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("RentItemIndex", new { rentWareHouseId = rentWareHouseItem.Id });
        }

         public async Task<IActionResult> EndRent(int? rentWareHouseId)
        {
            if (rentWareHouseId == null)
            {
                return NotFound();
            }

            var rentWareHouse = await _context.RentWareHouses
                .Where(m=>m.Id== rentWareHouseId)
                .FirstOrDefaultAsync();
            rentWareHouse.Status = 1;
            _context.Update(rentWareHouse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ResumeRent(int? rentWareHouseId)
        {
            if (rentWareHouseId == null)
            {
                return NotFound();
            }

            var rentWareHouse = await _context.RentWareHouses
                .Where(m=>m.Id== rentWareHouseId)
                .FirstOrDefaultAsync();
            rentWareHouse.Status = 0;
            _context.Update(rentWareHouse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }






    }
}
