using PP_RestaurantReview.Models;
using PP_RestaurantReview.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace PP_RestaurantReview.Controllers
{
    public class ReviewController : Controller
    {
        private static readonly HttpClient Client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ReviewController()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44322/api/");
        }

        // GET: Review/List
        public ActionResult List()
        {
            //objective: communicate with our review data api to retrieve a list of reviews
            string url = "reviewdata/listreviews";

            // a client is simply anything accessing info from server
            //a client can exist on a server, in this case our webserver is requesting info from out data access api
            //anticipating a respense

            HttpResponseMessage response = Client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode); //we have succesfully contacted our data access api

            ///making sure we retrieve the content from out response
            //objective is to parse response message into an ienumerable of type animal

            IEnumerable<ReviewDto> Reviews = response.Content.ReadAsAsync<IEnumerable<ReviewDto>>().Result;
            //Debug.WriteLine("number of reviews received: ");
            //Debug.WriteLine(reviews.Count());
            //this is the proper channel of communicate btwn web server in the review controller and the review data controller, communicating with api in the form of a http request

            return View(Reviews);
        }

        // GET: Review/Details/5
        public ActionResult Details(int id)
        {
            string url = "reviewdata/findreview/" + id;
            HttpResponseMessage response = Client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            ReviewDto SelectedReview = response.Content.ReadAsAsync<ReviewDto>().Result;
            //Debug.WriteLine("review received: ");
            //Debug.WriteLine(selectedreview.ReviewId);

            return View(SelectedReview);

        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Review/New
        // asks the user for info about the review
        public ActionResult New()
        {
            //NewReview ViewModel = new NewReview();
            //information about all the restaurants

            string url = "restaurantdata/listrestaurants";
            HttpResponseMessage response = Client.GetAsync(url).Result;
            IEnumerable<RestaurantDto> RestaurantOptions = response.Content.ReadAsAsync<IEnumerable<RestaurantDto>>().Result;
            //ViewModel.RestaurantOptions = RestaurantOptions;

            return View(RestaurantOptions);
        }

        // POST: Review/Create
        //responsible for creating the review
        [HttpPost]
        public ActionResult Create(Review review)
        {
            string url = "reviewdata/addreview";

            string jsonpayload = jss.Serialize(review); // will convert into json string

            //Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = Client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int id)
        {

            //the existing restaurant information
            UpdateReview ViewModel = new UpdateReview();

            string url = "reviewdata/findreview/" + id;
            HttpResponseMessage response = Client.GetAsync(url).Result;
            ReviewDto SelectedReview = response.Content.ReadAsAsync<ReviewDto>().Result;
            ViewModel.SelectedReview = SelectedReview;

            //also like to include all restaurants to choose from when i'm updating this animal
            url = "restaurantdata/listrestaurants";
            response = Client.GetAsync(url).Result;
            IEnumerable<RestaurantDto> RestaurantOptions = response.Content.ReadAsAsync<IEnumerable<RestaurantDto>>().Result;

            ViewModel.RestaurantOptions = RestaurantOptions;

            return View(ViewModel);
        }

        // POST: Review/Update/5
        [HttpPost]
        public ActionResult Update(int id, Review review)
        {
            string url = "reviewdata/updatereview/" + id;
            string jsonpayload = jss.Serialize(review);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = Client.PostAsync(url, content).Result;
            //Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Review/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "reviewdata/findreview/" + id;
            HttpResponseMessage response = Client.GetAsync(url).Result;
            ReviewDto SelectedReview = response.Content.ReadAsAsync<ReviewDto>().Result;
            return View(SelectedReview);
        }

        // POST: Review/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "reviewdata/deletereview/" + id;

            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = Client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
