using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquiadTradingApp.Models
{
    public class WareHouseTransfer
    {
        public int Id { get; set; }

        public string TransactionNo { get; set; }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }

        public int ToWareHouseId { get; set; }
        public WareHouse ToWareHouse { get; set; }
        
        public DateTime TransferDate { get; set; }


        public double TransferAmountPackSize { get; set; }
        public double TransferAmountInTons { get; set; }

        public int Status { get; set; }
        public string Remarks { get; set; }


    }
}
