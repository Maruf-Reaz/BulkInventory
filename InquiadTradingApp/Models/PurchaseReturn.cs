using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquiadTradingApp.Models
{
    public class PurchaseReturn
    {

        public int Id { get; set; }
        public string VoucharNo { get; set; }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }

        public double Amount { get; set; }

        public DateTime ReturnDate { get; set; }

        public int Status { get; set; }
        public double PackAmount { get; set; }
        public double TonAmount { get; set; }

        public string Remarks { get; set; }
    }
}
