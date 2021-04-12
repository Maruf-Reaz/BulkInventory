using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquiadTradingApp.Models
{
    public class WareHouse
    {
        public int Id { get; set; }
      
        public string Name { get; set; }
        public string Address { get; set; }

        public int Type { get; set; }                                  //0 = ships/port, 1= warehouses
    }
}
