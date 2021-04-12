using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquiadTradingApp.Models.ViewModels
{
    public class StockViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string ProductName { get; set; }
        public int? PackSizeId { get; set; }
        public string PackSizeName { get; set; }
        public int? WareHouseId { get; set; }
        public WareHouse WareHouse { get; set; }
        public string WareHouseName { get; set; }
        public int? PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public string PurchaseVoucharNo { get; set; }




        public double CurrentStockPack { get; set; }
        public double CurrentStockTon { get; set; }
    }
}
