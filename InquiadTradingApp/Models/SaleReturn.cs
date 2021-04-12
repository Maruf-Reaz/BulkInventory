using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquiadTradingApp.Models
{
    public class SaleReturn
    {
        public int Id { get; set; }
        public string VoucharNo { get; set; }

        public int SaleId { get; set; }
        public Sale Sale { get; set; }


        public double Amount { get; set; }
        public DateTime ReturnDate { get; set; }


        public double PackAmount { get; set; }
        public double TonAmount { get; set; }

        public int Status { get; set; }
        public string Remarks { get; set; }
    }
}
