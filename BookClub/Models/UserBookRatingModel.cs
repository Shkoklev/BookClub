using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookClub.Models
{
    public class UserBookRatingModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int BookId { get; set; }

        public int Value { get; set; }
    }
}