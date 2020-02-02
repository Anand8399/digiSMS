 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PresentationLayer.LocalResource;

namespace PresentationLayer.Areas.SysAdmin.Models
{
    public class AssetVM
    {
        [Key]
        public int SrNo { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "TypeofAsset", ResourceType = typeof(Resource))]
        public String TypeofAsset { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "AssetName", ResourceType = typeof(Resource))]
        public String AssetName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter quantity")]
        [Display(Name = "Quantity", ResourceType = typeof(Resource))]
        public int Quantity { get; set; }
     
         [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter price")]
        [Display(Name = "PricePerItem", ResourceType = typeof(Resource))]
        public decimal PricePerItem { get; set; }

        [Display(Name = "Total", ResourceType = typeof(Resource))]
        public decimal Total { get; set; }

        [Required]
        [Display(Name = "PurchaseDate", ResourceType = typeof(Resource))]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Condition", ResourceType = typeof(Resource))]
        public string Condition { get; set; }

        [Required]
        [Display(Name = "AssesmentYear", ResourceType = typeof(Resource))]
        public DateTime AssesmentYear { get; set; }

        [Required]
        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public bool Status { get; set; }

         [Display(Name = "Remark", ResourceType = typeof(Resource))]
        [StringLength(50)]
        public string Remark { get; set; }


         [Display(Name = "FromDate", ResourceType = typeof(Resource))]
         public DateTime FromDate { get; set; }
         [Display(Name = "ToDate", ResourceType = typeof(Resource))]
         public DateTime ToDate { get; set; }
        
    }
}