using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Cinema.CustomValidators;

namespace Cinema.Models
{
    public class Film
    {
        
        [Required]
        public int FilmID { get; set; }

        [Display(Name = "Film title")]
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Display(Name = "Release year")]
        [Required(ErrorMessage = "Year is required.")]
        [CorrectYear]
        public int Year { get; set; }

        [Display(Name = "Length (minutes)")]
        [Required(ErrorMessage = "Length is required.")]
        [Range(1, int.MaxValue)]
        public int Length { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
