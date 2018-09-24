using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ChangeAccessViewModel
    {
        public List<User> UserList { get; set; }
        public User UserSelected { get; set; }
    }
}