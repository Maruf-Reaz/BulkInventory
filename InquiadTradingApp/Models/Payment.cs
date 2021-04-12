using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquiadTradingApp.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public int Type { get; set; } //0 = cash , 1= cheque
        public string CheckNo { get; set; }
        public DateTime Date { get; set; }
        public double PaidAmount { get; set; }
        public int? PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public int? SaleId { get; set; }
        public Sale Sale { get; set; }


    }
}
