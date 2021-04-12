using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquiadTradingApp.Models
{
    public class DamageProduct
    {
        public int Id { get; set; }

        public DateTime DamageDate { get; set; }
        public string VoucharNo { get; set; }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }

        public double PackAmount { get; set; }
        public double TonAMount { get; set; }
        public double KGAmount { get; set; }

        public int Status { get; set; }
        public string Remarks { get; set; }
    }
}
