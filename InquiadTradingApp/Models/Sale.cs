using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InquiadTradingApp.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string VoucharNo { get; set; }

        //public int ProductId { get; set; }
        //public Product Product { get; set; }

        public int BrokerId { get; set; }
        public Broker Broker { get; set; }

        public double BrokerAMount { get; set; }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }

        public double PackAmount { get; set; }
        public double TonAmount { get; set; }
        


        public string ClientWareHouse { get; set; }
        public DateTime SaleDate { get; set; }

        [NotMapped]
        public DateTime PaymentDate { get; set; }
        [NotMapped]
        public int PaymentMethod { get; set; }          //0 = cash , 1= cheque
        [NotMapped]
        public string ChequeNo { get; set; }
        [NotMapped]
        public string PaymentDetails { get; set; }
        [NotMapped]
        public double PaidAmount { get; set; }
        public double TotalAmount { get; set; }
        
        public string Remarks { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int Status { get; set; }
        
        public List<Payment> Payments { get; set; }

    }
}
