using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ProfileUpdate
    {
        public User User { get; set; }
        public Garden Garden { get; set; }
        public UpdatePlantViewModel Plants {get; set;}
    }
}