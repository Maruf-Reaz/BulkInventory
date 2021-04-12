using InquiadTradingApp.Data;
using InquiadTradingApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquiadTradingApp.Models
{
    public class Product
    {
      
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProductCatagoryId { get; set; }
        public ProductCatagory ProductCatagory { get; set; }

      


       

    }
}
