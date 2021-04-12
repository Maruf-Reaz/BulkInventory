using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquiadTradingApp.Models.ViewModels
{
    public class MISViewModel
    {
        public string PurchaseVoucharNo { get; set; }
        public DateTime Date { get; set; }
        public string ProductName { get; set; }
        public string SupplierName { get; set; }


        public double PurchaseAmountTK { get; set; }
        public double PurchaseAmountTon { get; set; }

        public double SaleAmountTK { get; set; }
        public double SaleAmountTon { get; set; }

        
        public double DamageAmountTon { get; set; }
        public double StockAmountTon { get; set; }
        public double Profit { get; set; }



        

    }
}
