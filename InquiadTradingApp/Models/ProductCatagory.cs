using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquiadTradingApp.Models
{
    public class ProductCatagory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
