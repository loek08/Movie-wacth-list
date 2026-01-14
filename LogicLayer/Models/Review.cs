using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Models
{
    public class Review
    {
        public int Rating { get; set; }
        public string Comment { get; set; }

        public Review(int rating, string comment)
        {
            Rating = rating;
            Comment = comment;
        }

        public Review() { }

    }
}
