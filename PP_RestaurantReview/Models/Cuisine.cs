using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PP_RestaurantReview.Models
{
    public class Cuisine
    {
        [Key]
        public int CuisineId { get; set; }
        public string CuisineType { get; set; }
    }

    public class CuisineDto
    {
        public int CuisineId { get; set; }
        public string CuisineType { get; set; }
    }
}