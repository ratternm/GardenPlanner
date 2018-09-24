using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Plot : BaseItem
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public int Length { get; set; }
        [Required]
        public int GardenId { get; set; }
        [Required]
        public int HrsOfSun { get; set; }

        public int PlantId { get; set; } = 0;

        public double ? PlotCost { get; set; }

        public Plant Plant { get; set; }

        public int SizeSqInch
        {
            get
            {
                return (Width * Length) * 12;
            }
            set
            {

            }
        }
    }
}