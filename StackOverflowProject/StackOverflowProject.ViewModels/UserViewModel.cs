using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StackOverflowProject.ViewModels
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        [Phone]
        public string Mobile { get; set; }
        public bool IsAdmin { get; set; }
    }
}
