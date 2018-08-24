using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Cinema.CustomValidators;

namespace Cinema.Models
{
    public class Customer
    {
        [Required]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Capitalize]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Capitalize]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
