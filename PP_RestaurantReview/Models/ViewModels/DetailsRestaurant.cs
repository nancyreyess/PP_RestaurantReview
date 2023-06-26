using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PP_RestaurantReview.Models.ViewModels
{
    public class DetailsRestaurant
    {
        public RestaurantDto SelectedRestaurant { get; set; }
        public IEnumerable<ReviewDto> RelatedReviews { get; set; }
    }
}