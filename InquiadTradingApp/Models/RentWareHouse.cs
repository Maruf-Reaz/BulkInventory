using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquiadTradingApp.Models
{
    public class RentWareHouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public int Status { get; set; }
        public List<RentWareHouseItem> RentWareHouseItems { get; set; }
    }
}
