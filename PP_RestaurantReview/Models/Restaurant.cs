using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PP_RestaurantReview.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }


        [ForeignKey("Cuisine")]
        public int CuisineId { get; set; }
        public virtual Cuisine Cuisine { get; set; }
        

        public string Description { get; set; }
        public string Address { get; set; }
        public string RestaurantLink { get; set; }
    }

    public class RestaurantDto
    {
        //fk above making the issues
        //create simpler version of the class,and return that simpler version of the data
        //as a vessel from api to end user
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string RestaurantLink { get; set; }

        public int CuisineId { get; set; }
        public string CuisineType { get; set; }
    }
}