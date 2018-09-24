using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Garden : BaseItem
    {
        [Required(ErrorMessage = "Please enter your garden's name.")]
        [MaxLength(20, ErrorMessage = "Garden's Name has a max length of 20 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Input must be a whole number")]
        public int TempLow { get; set; }

        [Required(ErrorMessage = "Please enter a number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Input must be a whole number")]
        public int TempHigh { get; set; }
        
        public int UserId { get; set; }
    }
}