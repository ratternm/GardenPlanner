using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Role : BaseItem
    {
        public string RoleName { get; set; }

        public Role Clone()
        {
            Role item = new Role();
            item.Id = this.Id;
            item.RoleName = this.RoleName;
            return item;
        }
    }
}