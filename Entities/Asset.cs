using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Asset
    {
        public int SrNo { get; set; }

        public string TypeofAsset { get; set; }

       public string AssetName { get; set; }

         public int Quantity { get; set; }

        public decimal PricePerItem { get; set; }

        public decimal Total { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Condition { get; set; }

        public DateTime AssesmentYear { get; set; }

        public bool Status { get; set; }

        public string Remark { get; set; }
   
    }
}
