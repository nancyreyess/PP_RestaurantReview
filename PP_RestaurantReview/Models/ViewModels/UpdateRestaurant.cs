using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PP_RestaurantReview.Models.ViewModels
{
    public class UpdateRestaurant
    {
        public RestaurantDto SelectedRestaurant { get; set; }

        //all species to choose from when updating this review

        public IEnumerable<CuisineDto> CuisineOptions { get; set; }
    }
}