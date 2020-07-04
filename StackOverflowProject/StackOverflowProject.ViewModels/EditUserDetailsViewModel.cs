using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StackOverflowProject.ViewModels
{
    public class EditUserDetailsViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[A-za-z]*$")]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string Mobile { get; set; }
    }
}
