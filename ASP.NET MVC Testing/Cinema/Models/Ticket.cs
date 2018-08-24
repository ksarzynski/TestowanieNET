using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class Ticket
    {
        [Required]
        public int TicketID { get; set; }

        [Required]
        public int FilmID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Display(Name = "Ticket price")]
        [Required(ErrorMessage = "Price is required.")]
        [Range(0, 9999.99)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Screening date")]
        [Required(ErrorMessage = "Date is required.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Film title")]
        public Film Film { get; set; }

        [Display(Name = "Customer's email")]
        public Customer Customer { get; set; }
    }
}
