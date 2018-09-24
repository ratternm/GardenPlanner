using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class CreatePlotView
    {
        public int PlotId { get; set; }

        [Required]
        public string PlotName { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Input must be a whole number")]
        public int Width { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Input must be a whole number")]
        public int Length { get; set; }

        [Required]
        public int GardenId { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Input must be a whole number")]
        public int HrsOfSun { get; set; }

        public int PlantId { get; set; }

        [Required]
        public string PlantName { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Input must be a whole number")]
        public int? SizeSq { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Input must be a whole number")]
        public int? TempLow { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Input must be a whole number")]
        public int? TempHigh { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Input must be a whole number")]
        public int? Cost { get; set; }

        public void AssignPlot(Plot plot)
        {
            this.PlotId = plot.Id;
            this.PlantName = plot.Name;
            this.Width = plot.Width;
            this.Length = plot.Length;
            this.GardenId = plot.GardenId;
            this.HrsOfSun = plot.HrsOfSun;
        }

        public void AssignPlant(Plant plant)
        {
            this.PlantId = plant.Id;
            this.PlantName = plant.Name;
            this.SizeSq = plant.SizeSq;
            this.TempHigh = plant.TempHigh;
            this.TempLow = plant.TempLow;
            this.Cost = plant.Cost;
        }

    }




}