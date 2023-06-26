using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PP_RestaurantReview.Models.ViewModels
{
    public class DetailsCuisine
    {
        public CuisineDto SelectedCuisine { get; set; }
        public IEnumerable<RestaurantDto> RelatedRestaurants { get; set; }
    }
}