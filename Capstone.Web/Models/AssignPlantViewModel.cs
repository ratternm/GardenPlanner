using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class AssignPlantViewModel
    {
        public List<Plant> Plants { get; set; }
        public List<Materials> Materials { get; set; }
       
        public int GardenHighTemp { get; set; }
        public int GardenLowTemp { get; set; }
        public Plot UserPlot { get; set; }
        public double PlotCost { get; set; }
    }
}