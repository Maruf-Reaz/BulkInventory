using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquiadTradingApp.Models
{
    public class RentWareHouseItem
    {
        public int Id { get; set; }
        public string Details { get; set; }

        public double Amount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }

        public int RentWareHouseId { get; set; }
        public RentWareHouse RentWareHouse { get; set; }

    }
}
