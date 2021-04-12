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
    public class WareHouseTransfersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WareHouseTransfersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WareHouseTransfers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WareHouseTransfers
                .Include(w => w.Purchase)
                .Include(w => w.Purchase.Product)
                .Include(w => w.Purchase.WareHouse)
                .Include(w => w.Purchase.PackSize)
                .Include(w => w.ToWareHouse)
                  .OrderByDescending(m => m.TransferDate);
            return View(await applicationDbContext.ToListAsync());
        }



        public async Task<IActionResult> WarehouseTransferStatement(DateTime fromDate, DateTime toDate)
        {
            if (fromDate == default(DateTime))
            {
                fromDate = DateTime.Now.AddDays(-30);
            }
            if (toDate == default(DateTime))
            {
                toDate = DateTime.Now.Date;
            }

            var warehouseTrabsfers = await _context.WareHouseTransfers

                .Include(w => w.Purchase)
                .Include(w => w.Purchase.Product)
                .Include(w => w.Purchase.WareHouse)
                .Include(w => w.Purchase.PackSize)
                .Include(w => w.ToWareHouse)
                  .OrderByDescending(m => m.TransferDate)
             .Where(m => m.TransferDate >= fromDate && m.TransferDate <= toDate && m.Status == 1)
             .ToListAsync();

            ViewData["Transfers"] = warehouseTrabsfers;
            ViewData["FromDate"] = fromDate.Date;
            ViewData["ToDate"] = toDate.Date;
            return View();
        }
        [HttpPost]
        public IActionResult WarehouseTransferStatement(FromDateToDateViewModel datesVM)
        {

            return RedirectToAction("WarehouseTransferStatement", new { id = datesVM.Id, fromDate = datesVM.FromDate, toDate = datesVM.ToDate });

        }



        // GET: WareHouseTransfers/Create
        public IActionResult Create()
        {
            ViewData["PurchaseId"] = new SelectList(_context.Purchases.Where(m => m.Status == 1), "Id", "VoucharNo");
            ViewData["ToWareHouseId"] = new SelectList(_context.WareHouses, "Id", "Name");
            return View();
        }

        // POST: WareHouseTransfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WareHouseTransfer wareHouseTransfer)
        {
            int count = _context.SaleReturns.Count() + 1;
            wareHouseTransfer.TransactionNo = "WHTV000" + count.ToString();
            wareHouseTransfer.Status = 0;
            if (ModelState.IsValid)
            {
                _context.Add(wareHouseTransfer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PurchaseId"] = new SelectList(_context.Purchases.Where(m => m.Status == 1), "Id", "Name");
            ViewData["ToWareHouseId"] = new SelectList(_context.WareHouses, "Id", "Name", wareHouseTransfer.ToWareHouseId);

            return View(wareHouseTransfer);
        }



        // GET: WareHouseTransfers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wareHouseTransfer = await _context.WareHouseTransfers
                .Include(w => w.Purchase)
                .Include(w => w.Purchase.WareHouse)
                .Include(w => w.Purchase.Product)
                .Include(w => w.Purchase.PackSize)
                .Include(w => w.ToWareHouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wareHouseTransfer == null)
            {
                return NotFound();
            }

            return View(wareHouseTransfer);
        }

        // POST: WareHouseTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wareHouseTransfer = await _context.WareHouseTransfers.FindAsync(id);
            _context.WareHouseTransfers.Remove(wareHouseTransfer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WareHouseTransferExists(int id)
        {
            return _context.WareHouseTransfers.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ApproveTransfer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transfer = await _context.WareHouseTransfers
                .FirstOrDefaultAsync(m => m.Id == id);




            if (transfer == null)
            {
                return NotFound();
            }
            else if (transfer.Status != 0)
            {
                return NotFound();
            }
            else
            {
                var purchase = await _context.Purchases.Where(m => m.Id == transfer.PurchaseId).FirstOrDefaultAsync();

                purchase.WareHouseId = transfer.ToWareHouseId;
                _context.Update(purchase);
                int result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    transfer.Status = 1;
                    _context.Update(transfer);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }

        }
    }
}
