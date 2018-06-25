using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookClub.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public int Year { get; set; }

        public string Synopsis { get; set; }

        public int Votes { get; set; }

        public int Rating { get; set; }

        public float AverageRating { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public Book()
        {
            Users = new List<ApplicationUser>();
        }
    }
}