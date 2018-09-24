using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Materials
    {
        public string MatName { get; set; }
        public decimal MatCost { get; set; }
        public string Description { get; set; }
        public int ? MatQty { get; set;  }
    }
}