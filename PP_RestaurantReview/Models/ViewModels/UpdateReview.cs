using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PP_RestaurantReview.Models.ViewModels
{
    public class UpdateReview
    {
        //this viewmodel is a class which stores information that we need to present to /Review/Update/{}

        //the existing review information

        public ReviewDto SelectedReview { get; set; }

        //all restaurants to choose from when updating this review

        public IEnumerable<RestaurantDto> RestaurantOptions { get; set; }

    }
}