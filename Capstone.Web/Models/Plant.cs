using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Plant : BaseItem
    {
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Square Inches
        /// </summary>
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Input must be a whole number")]
        public int ? SizeSq { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Input must be a whole number")]
        public int ? TempLow { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Input must be a whole number")]
        public int ? TempHigh { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Input must be a whole number")]
        public int ? Cost { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Input must be a whole number")]
        public int SunReqHrs { get; set; }
    }
}