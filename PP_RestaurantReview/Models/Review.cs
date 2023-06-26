using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PP_RestaurantReview.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public DateTime ReviewDate { get; set; }
        public int ReviewRating { get; set; }
        public string ReviewComment { get; set; }

        //A restaurant can have multiple reviews, but a review can only belong to one restaurant
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        //needs to change to a ForeignKey from ApplicationUser
        public int UserId { get; set; }
        
    }

    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public DateTime ReviewDate { get; set; }
        public int ReviewRating { get; set; }
        public string ReviewComment { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public int UserId { get; set; }
    }
}