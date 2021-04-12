using InquiadTradingApp.Data;
using InquiadTradingApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquiadTradingApp.Models
{
    public class StockMethod
    {
        private readonly ApplicationDbContext _context;

        public StockMethod(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<StockViewModel> GetAllProductStock()
        {
            //var products = _context.Products
            //   .Include(i => i.PackSize)
            //   .Include(i => i.ProductCatagory)
            //   .ToList();


            //List<StockViewModel> stockViewModels = new List<StockViewModel>();

            //foreach (var product in products)
            //{
            //    StockViewModel stockViewModel = new StockViewModel();
            //    var allPurchases = _context.Purchases
            //        .Where(m => m.ProductId == product.Id && m.Status == 1)
            //        .ToList();

            //    var allPurchaseReturns = _context.PurchaseReturns
            //      .Where(m => m.Purchase.ProductId == product.Id && m.Status == 1)
            //      .ToList();



            //    var allSales = _context.Sales
            //       .Where(m => m.Purchase.ProductId == product.Id && m.Status == 1)
            //       .ToList();
            //    var allSaleReturns = _context.SaleReturns
            //       .Where(m => m.Sale.Purchase.ProductId == product.Id && m.Status == 1)
            //       .ToList();


            //    double totalPurchasePack = (double)allPurchases.Sum(m => m.PackAmount);
            //    double totalPurchaseTon = (double)allPurchases.Sum(m => m.TonAmount);

            //    double totalPurchaseReturnPack = (double)allPurchaseReturns.Sum(m => m.PackAmount);
            //    double totalPurchaseReturnTon = (double)allPurchaseReturns.Sum(m => m.TonAmount);

            //    double totalSalePack = (double)allSales.Sum(m => m.TonAmount);
            //    double totalSaleTon = (double)allSales.Sum(m => m.TonAmount);

            //    double totalSaleReturnPack = (double)allSaleReturns.Sum(m => m.PackAmount);
            //    double totalSaleReturnTon = (double)allSaleReturns.Sum(m => m.TonAmount);

            //    double currentStockPack = (totalPurchasePack - totalPurchaseReturnPack) - (totalSalePack - totalSaleReturnPack);
            //    double currentStockTon = (totalPurchaseTon - totalPurchaseReturnTon) - (totalSaleTon - totalSaleReturnTon);


            //    stockViewModel.Product = product;
            //    stockViewModel.ProductId = product.Id;

            //    stockViewModel.CurrentStockPack = currentStockPack;
            //    stockViewModel.CurrentStockTon = currentStockTon;
            //    stockViewModels.Add(stockViewModel);

            //}
            // return stockViewModels;
            return null;

        }



        public StockViewModel GetPurchaseStock(int id)
        {
            var purchase = _context.Purchases
               .Include(i => i.WareHouse)
               .Include(i => i.Product)
               .Include(i => i.PackSize)
               .Include(i => i.Product.ProductCatagory)
               .Where(m => m.Id == id)
               .FirstOrDefault();


            if (purchase == null)
            {
                return null;
            }
            var sales = _context.Sales
              .Include(i => i.Purchase)
              .Where(m => m.PurchaseId == id && m.Status == 1)
              .ToList();
            var saleReturns = _context.SaleReturns
             .Include(i => i.Sale.Purchase)
             .Where(m => m.Sale.PurchaseId == id && m.Status == 1)
             .ToList();

            var purchaseReturns = _context.PurchaseReturns
            .Include(i => i.Purchase)
            .Where(m => m.PurchaseId == id && m.Status == 1)
            .ToList();

             var damageProducts = _context.DamageProducts
            .Include(i => i.Purchase)
            .Where(m => m.PurchaseId == id && m.Status == 1)
            .ToList();

             var localSales = _context.LocalSales
            .Include(i => i.Purchase)
            .Where(m => m.PurchaseId == id && m.Status == 1)
            .ToList();




            double tonAmountSale = (double)sales.Sum(m => m.TonAmount);
            double packAmountSale = (double)sales.Sum(m => m.PackAmount);

            double tonAmountSaleReturn = (double)saleReturns.Sum(m => m.TonAmount);
            double packAmountSaleReturn = (double)saleReturns.Sum(m => m.PackAmount);

            double tonAmountPurchaseReturn = (double)purchaseReturns.Sum(m => m.TonAmount);
            double packAmountPurchaseReturn = (double)purchaseReturns.Sum(m => m.PackAmount);



             double tonAmountDamage = (double)damageProducts.Sum(m => m.TonAMount);
            double packAmountDamage = (double)damageProducts.Sum(m => m.PackAmount);

              double tonAmountLocalSale = (double)localSales.Sum(m => m.TonAMount);
            double packAmountLocalSale = (double)localSales.Sum(m => m.PackAmount);
            

            StockViewModel stockViewModel = new StockViewModel();

            stockViewModel.PurchaseId = purchase.Id;
            stockViewModel.PurchaseVoucharNo = purchase.VoucharNo;
            stockViewModel.PackSizeId = purchase.PackSizeId;
            stockViewModel.PackSizeName = purchase.PackSize.Name;
            stockViewModel.WareHouseId = purchase.WareHouseId;
            stockViewModel.WareHouseName = purchase.WareHouse.Name;
            stockViewModel.ProductId = purchase.ProductId;
            stockViewModel.ProductName = purchase.Product.Name;

            stockViewModel.CurrentStockPack = (purchase.PackAmount - packAmountPurchaseReturn) - (packAmountSale - packAmountSaleReturn)-packAmountLocalSale-packAmountDamage;

            double finalPurchase = purchase.TonAmount - tonAmountPurchaseReturn;
            double finalSale = tonAmountSale - tonAmountSaleReturn;

            double basicStock = Math.Round(finalPurchase - finalSale, 2);
            stockViewModel.CurrentStockTon = basicStock - tonAmountLocalSale-tonAmountDamage;


            return stockViewModel;

        }

        public List<StockViewModel> GetProductStock(int id)
        {
            


            var purchases = _context.Purchases
               .Include(i => i.WareHouse)
               .Include(i => i.Product)
               .Include(i => i.PackSize)
               .Include(i => i.Product.ProductCatagory)
               .Where(m => m.ProductId == id)
               .ToList();


            if (purchases == null)
            {
                return null;
            }
            List<StockViewModel> stockViewModels = new List<StockViewModel>();
            foreach (var purchase in purchases)
            {
                var sales = _context.Sales
              .Include(i => i.Purchase)
              .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
              .ToList();
                var saleReturns = _context.SaleReturns
                 .Include(i => i.Sale.Purchase)
                 .Where(m => m.Sale.PurchaseId == purchase.Id && m.Status == 1)
                 .ToList();

                var purchaseReturns = _context.PurchaseReturns
                .Include(i => i.Purchase)
                .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
                .ToList();

                var damageProducts = _context.DamageProducts
               .Include(i => i.Purchase)
               .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
               .ToList();

                var localSales = _context.LocalSales
               .Include(i => i.Purchase)
               .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
               .ToList();




                double tonAmountSale = (double)sales.Sum(m => m.TonAmount);
                double packAmountSale = (double)sales.Sum(m => m.PackAmount);

                double tonAmountSaleReturn = (double)saleReturns.Sum(m => m.TonAmount);
                double packAmountSaleReturn = (double)saleReturns.Sum(m => m.PackAmount);

                double tonAmountPurchaseReturn = (double)purchaseReturns.Sum(m => m.TonAmount);
                double packAmountPurchaseReturn = (double)purchaseReturns.Sum(m => m.PackAmount);



                double tonAmountDamage = (double)damageProducts.Sum(m => m.TonAMount);
                double packAmountDamage = (double)damageProducts.Sum(m => m.PackAmount);

                double tonAmountLocalSale = (double)localSales.Sum(m => m.TonAMount);
                double packAmountLocalSale = (double)localSales.Sum(m => m.PackAmount);


                StockViewModel stockViewModel = new StockViewModel();

                stockViewModel.PurchaseId = purchase.Id;
                stockViewModel.PurchaseVoucharNo = purchase.VoucharNo;
                stockViewModel.PackSizeId = purchase.PackSizeId;
                stockViewModel.PackSizeName = purchase.PackSize.Name;
                stockViewModel.WareHouseId = purchase.WareHouseId;
                stockViewModel.WareHouseName = purchase.WareHouse.Name;
                stockViewModel.ProductId = purchase.ProductId;
                stockViewModel.ProductName = purchase.Product.Name;

                stockViewModel.CurrentStockPack = (purchase.PackAmount - packAmountPurchaseReturn) - (packAmountSale - packAmountSaleReturn) - packAmountLocalSale - packAmountDamage;

                double finalPurchase = purchase.TonAmount - tonAmountPurchaseReturn;
                double finalSale = tonAmountSale - tonAmountSaleReturn;

                double basicStock = Math.Round(finalPurchase - finalSale, 2);
                stockViewModel.CurrentStockTon = basicStock - tonAmountLocalSale - tonAmountDamage;

              
                stockViewModels.Add(stockViewModel);

            }




            return stockViewModels;

        }
        public List<MISViewModel> GetMisReport(List<Purchase> purchases)
        {
            
            

            if (purchases == null)
            {
                return null;
            }
            List<MISViewModel> misViewModels = new List<MISViewModel>();
            foreach (var purchase in purchases)
            {
                var sales = _context.Sales
              .Include(i => i.Purchase)
              .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
              .ToList();
                var saleReturns = _context.SaleReturns
                 .Include(i => i.Sale.Purchase)
                 .Where(m => m.Sale.PurchaseId == purchase.Id && m.Status == 1)
                 .ToList();

                var purchaseReturns = _context.PurchaseReturns
                .Include(i => i.Purchase)
                .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
                .ToList();

                var damageProducts = _context.DamageProducts
               .Include(i => i.Purchase)
               .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
               .ToList();

                var localSales = _context.LocalSales
               .Include(i => i.Purchase)
               .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
               .ToList();




                double tonAmountSale = (double)sales.Sum(m => m.TonAmount);
                double tkAmountSale = (double)sales.Sum(m => m.TotalAmount);

                double tonAmountSaleReturn = (double)saleReturns.Sum(m => m.TonAmount);
                double tkAmountSaleReturn = (double)saleReturns.Sum(m => m.Amount);

                double tonAmountPurchaseReturn = (double)purchaseReturns.Sum(m => m.TonAmount);
                double tkAmountPurchaseReturn = (double)purchaseReturns.Sum(m => m.Amount);



                double tonAmountDamage = (double)damageProducts.Sum(m => m.TonAMount);

                double tonAmountLocalSale = (double)localSales.Sum(m => m.TonAMount);
                double tkAmountLocalSale = (double)localSales.Sum(m => m.Amount);


                MISViewModel misViewModel = new MISViewModel();

                misViewModel.PurchaseVoucharNo = purchase.VoucharNo;
                misViewModel.Date = purchase.PurchaseDate;
                misViewModel.ProductName = purchase.Product.Name;
                misViewModel.SupplierName = purchase.Vendor.Name;
                misViewModel.PurchaseAmountTK = purchase.TotalAmount-tkAmountPurchaseReturn;
                misViewModel.PurchaseAmountTon = purchase.TonAmount - tonAmountPurchaseReturn;
                misViewModel.SaleAmountTon = tonAmountSale +tonAmountLocalSale -tonAmountSaleReturn;
                misViewModel.SaleAmountTK = tkAmountSale+tkAmountLocalSale-tkAmountSaleReturn;
                misViewModel.DamageAmountTon = tonAmountDamage;

                misViewModel.StockAmountTon = (purchase.TonAmount - tonAmountPurchaseReturn) - (tonAmountSale - tonAmountSaleReturn) - tonAmountLocalSale - tonAmountDamage;

                double perTonPurchase = misViewModel.PurchaseAmountTK / misViewModel.PurchaseAmountTon;
                double perTonSale = misViewModel.SaleAmountTK / misViewModel.SaleAmountTon; ;
                misViewModel.Profit = perTonSale-perTonPurchase;



                misViewModels.Add(misViewModel);

            }
            
            return misViewModels;

        }
        public List<MISViewModel> GetMISReportPercent(List<Purchase> purchases)
        {
            
            

            if (purchases == null)
            {
                return null;
            }
            List<MISViewModel> misViewModels = new List<MISViewModel>();
            foreach (var purchase in purchases)
            {
                var sales = _context.Sales
              .Include(i => i.Purchase)
              .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
              .ToList();
                var saleReturns = _context.SaleReturns
                 .Include(i => i.Sale.Purchase)
                 .Where(m => m.Sale.PurchaseId == purchase.Id && m.Status == 1)
                 .ToList();

                var purchaseReturns = _context.PurchaseReturns
                .Include(i => i.Purchase)
                .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
                .ToList();

                var damageProducts = _context.DamageProducts
               .Include(i => i.Purchase)
               .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
               .ToList();

                var localSales = _context.LocalSales
               .Include(i => i.Purchase)
               .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
               .ToList();




                double tonAmountSale = (double)sales.Sum(m => m.TonAmount);
                double tkAmountSale = (double)sales.Sum(m => m.TotalAmount);

                double tonAmountSaleReturn = (double)saleReturns.Sum(m => m.TonAmount);
                double tkAmountSaleReturn = (double)saleReturns.Sum(m => m.Amount);

                double tonAmountPurchaseReturn = (double)purchaseReturns.Sum(m => m.TonAmount);
                double tkAmountPurchaseReturn = (double)purchaseReturns.Sum(m => m.Amount);



                double tonAmountDamage = (double)damageProducts.Sum(m => m.TonAMount);

                double tonAmountLocalSale = (double)localSales.Sum(m => m.TonAMount);
                double tkAmountLocalSale = (double)localSales.Sum(m => m.Amount);


                MISViewModel misViewModel = new MISViewModel();

                misViewModel.PurchaseVoucharNo = purchase.VoucharNo;
                misViewModel.Date = purchase.PurchaseDate;
                misViewModel.ProductName = purchase.Product.Name;
                misViewModel.SupplierName = purchase.Vendor.Name;
                misViewModel.PurchaseAmountTK = purchase.TotalAmount-tkAmountPurchaseReturn;
                misViewModel.PurchaseAmountTon = purchase.TonAmount - tonAmountPurchaseReturn;
                misViewModel.SaleAmountTon = tonAmountSale +tonAmountLocalSale -tonAmountSaleReturn;
                misViewModel.SaleAmountTK = tkAmountSale+tkAmountLocalSale-tkAmountSaleReturn;
                misViewModel.DamageAmountTon = tonAmountDamage;

                misViewModel.StockAmountTon = (purchase.TonAmount - tonAmountPurchaseReturn) - (tonAmountSale - tonAmountSaleReturn) - tonAmountLocalSale - tonAmountDamage;

                double perTonPurchase = misViewModel.PurchaseAmountTK / misViewModel.PurchaseAmountTon;
                double perTonSale = misViewModel.SaleAmountTK / misViewModel.SaleAmountTon;

                double profit = perTonSale - perTonPurchase;


                misViewModel.Profit = (profit/ perTonPurchase)*100;



                misViewModels.Add(misViewModel);

            }
            
            return misViewModels;

        }

        public List<StockViewModel> GetWareHouseStock(int id)
        {
            


            var purchases = _context.Purchases
               .Include(i => i.WareHouse)
               .Include(i => i.Product)
               .Include(i => i.PackSize)
               .Include(i => i.Product.ProductCatagory)
               .Where(m => m.WareHouseId == id)
               .ToList();


            if (purchases == null)
            {
                return null;
            }
            List<StockViewModel> stockViewModels = new List<StockViewModel>();
            foreach (var purchase in purchases)
            {
                var sales = _context.Sales
              .Include(i => i.Purchase)
              .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
              .ToList();
                var saleReturns = _context.SaleReturns
                 .Include(i => i.Sale.Purchase)
                 .Where(m => m.Sale.PurchaseId == purchase.Id && m.Status == 1)
                 .ToList();

                var purchaseReturns = _context.PurchaseReturns
                .Include(i => i.Purchase)
                .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
                .ToList();

                var damageProducts = _context.DamageProducts
               .Include(i => i.Purchase)
               .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
               .ToList();

                var localSales = _context.LocalSales
               .Include(i => i.Purchase)
               .Where(m => m.PurchaseId == purchase.Id && m.Status == 1)
               .ToList();




                double tonAmountSale = (double)sales.Sum(m => m.TonAmount);
                double packAmountSale = (double)sales.Sum(m => m.PackAmount);

                double tonAmountSaleReturn = (double)saleReturns.Sum(m => m.TonAmount);
                double packAmountSaleReturn = (double)saleReturns.Sum(m => m.PackAmount);

                double tonAmountPurchaseReturn = (double)purchaseReturns.Sum(m => m.TonAmount);
                double packAmountPurchaseReturn = (double)purchaseReturns.Sum(m => m.PackAmount);



                double tonAmountDamage = (double)damageProducts.Sum(m => m.TonAMount);
                double packAmountDamage = (double)damageProducts.Sum(m => m.PackAmount);

                double tonAmountLocalSale = (double)localSales.Sum(m => m.TonAMount);
                double packAmountLocalSale = (double)localSales.Sum(m => m.PackAmount);


                StockViewModel stockViewModel = new StockViewModel();

                stockViewModel.PurchaseId = purchase.Id;
                stockViewModel.PurchaseVoucharNo = purchase.VoucharNo;
                stockViewModel.PackSizeId = purchase.PackSizeId;
                stockViewModel.PackSizeName = purchase.PackSize.Name;
                stockViewModel.WareHouseId = purchase.WareHouseId;
                stockViewModel.WareHouseName = purchase.WareHouse.Name;
                stockViewModel.ProductId = purchase.ProductId;
                stockViewModel.ProductName = purchase.Product.Name;

                stockViewModel.CurrentStockPack = (purchase.PackAmount - packAmountPurchaseReturn) - (packAmountSale - packAmountSaleReturn) - packAmountLocalSale - packAmountDamage;
                double finalPurchase = purchase.TonAmount - tonAmountPurchaseReturn;
                double finalSale = tonAmountSale - tonAmountSaleReturn;

                double basicStock = Math.Round(finalPurchase - finalSale, 2);
                stockViewModel.CurrentStockTon = basicStock - tonAmountLocalSale - tonAmountDamage;
                stockViewModels.Add(stockViewModel);

            }




            return stockViewModels;

        }





    }
}
