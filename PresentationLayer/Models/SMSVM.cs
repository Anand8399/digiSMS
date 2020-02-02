using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class SMSVM
    {
        public long UserId { get; set; }

        public string Password { get; set; }

        public long MobileNoTo { get; set; }

        public string Message { get; set; }
    }
}