using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookClub.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Synopsis { get; set; }

        public int Votes { get; set; }

        public int Rating { get; set; }

        public float AverageRating { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public Book()
        {
            Users = new List<ApplicationUser>();
        }
    }
}