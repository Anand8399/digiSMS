using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class HomeChartVM
    {
        public List<String> classList { get; set; }
        public List<int> classwiseBoysList { get; set; }
        public List<int> classwiseGirlsList { get; set; }
      
	    public int CastGeneralCount { get; set; }
        public int CastNT1Count { get; set; }
        public int CastNT2Count { get; set; }
        public int CastNT3Count { get; set; }
        public int CastNT4Count { get; set; }
        public int CastOBCCount { get; set; }
        public int CastSBCCount { get; set; }
        public int CastSCCount { get; set; }
        public int CastSTCount { get; set; }
        public int CastVJCount { get; set; }
        public int CastVJ1Count { get; set; }

        

    }
}