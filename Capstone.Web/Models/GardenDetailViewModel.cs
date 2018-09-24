using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class GardenDetailViewModel : BaseItem
    {
        private List<Plot> _plots = new List<Plot>();

        public string Name { get; set; }
        public int TempLow { get; set; }
        public int TempHigh { get; set; }
        public int UserId { get; set; }
        public Plant Plant {get; set; }
        

        public List<Plot> GardenPlots
        {
            get
            {
                return _plots;
            }
            set
            {
                _plots.AddRange(value); 
            }
        }

        public void AssignGarden(Garden garden)
        {
            this.Id = garden.Id;
            this.Name = garden.Name;
            this.TempLow = garden.TempLow;
            this.TempHigh = garden.TempHigh;
            this.UserId = garden.UserId;
        }
    }
}