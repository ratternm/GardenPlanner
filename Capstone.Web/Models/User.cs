using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class User : BaseItem
    {
        [Required(ErrorMessage = "Please enter your first name.")]
        [MaxLength(20, ErrorMessage = "First Name has a max length of 20 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        [MaxLength(20, ErrorMessage = "Last Name has a max length of 20 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please use a valid Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords did not match.")]
        public string ConfirmPassword { get; set; }

        public string Salt { get; set; }
        public int RoleId { get; set; }

        public User Clone()
        {
            User item = new User();
            item.Id = this.Id;
            item.FirstName = this.FirstName;
            item.LastName = this.LastName;
            item.Email = this.Email;
            item.Password = this.Password;
            item.Salt = this.Salt;
            item.RoleId = this.RoleId;
            return item;
        }
    }
}