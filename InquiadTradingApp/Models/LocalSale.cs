using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquiadTradingApp.Models
{
    public class LocalSale
    {
        public int Id { get; set; }

        public DateTime SaleDate { get; set; }
        public int PurchaseId { get; set; }
        public string VoucharNo { get; set; }
        public Purchase Purchase { get; set; }
        public string ClientName { get; set; }
        public double PackAmount { get; set; }
        public double TonAMount { get; set; }
        public double Amount { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
    }
}
