using PP_RestaurantReview.Migrations;
using PP_RestaurantReview.Models;
using PP_RestaurantReview.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PP_RestaurantReview.Controllers
{
    public class CuisineController : Controller
    {
        private static readonly HttpClient Client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static CuisineController()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44322/api/");
        }
        // GET: Cuisine/List
        public ActionResult List()
        {
            string url = "cuisinedata/listcuisines";

            HttpResponseMessage response = Client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<CuisineDto> Cuisines = response.Content.ReadAsAsync<IEnumerable<CuisineDto>>().Result;

            return View(Cuisines);
        }

        // GET: Cuisine/Details/5
        public ActionResult Details(int id)
        {
            DetailsCuisine ViewModel = new DetailsCuisine();

            string url = "cuisinedata/findcuisine/" + id;
            HttpResponseMessage response = Client.GetAsync(url).Result;

            CuisineDto SelectedCuisine = response.Content.ReadAsAsync<CuisineDto>().Result;

            ViewModel.SelectedCuisine = SelectedCuisine;

            //showcase information about restaurants related to this cuisine
            // send request to gather information about restaurnts related to a particular cuisine id
            url = "restaurantdata/listrestaurantsforcuisine/" + id;
            response = Client.GetAsync(url).Result;
            IEnumerable<RestaurantDto> RelatedRestaurants = response.Content.ReadAsAsync<IEnumerable<RestaurantDto>>().Result;

            ViewModel.RelatedRestaurants = RelatedRestaurants;

            return View(ViewModel);
        }

        //Error page
        public ActionResult Error()
        {
            return View();
        }

        // GET: Cuisine/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Cuisine/Create
        [HttpPost]
        public ActionResult Create(Cuisine cuisine)
        {
            string url = "cuisinedata/addcuisine";

            string jsonpayload = jss.Serialize(cuisine); // will convert into json string
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

        // GET: Cuisine/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "cuisinedata/findcuisine/" + id;
            HttpResponseMessage response = Client.GetAsync(url).Result;
            CuisineDto SelectedCuisine = response.Content.ReadAsAsync<CuisineDto>().Result;
            return View(SelectedCuisine);
        }

        // POST: Cuisine/Update/5
        [HttpPost]
        public ActionResult Update(int id, Cuisine cuisine)
        {
            string url = "cuisinedata/updatercuisine/" + id;
            string jsonpayload = jss.Serialize(cuisine);
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

        // GET: Cuisine/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "cuisinedata/findcuisine/" + id;
            HttpResponseMessage response = Client.GetAsync(url).Result;
            CuisineDto SelectedCuisine = response.Content.ReadAsAsync<CuisineDto>().Result;
            return View(SelectedCuisine);
        }

        // POST: Cuisine/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "cuisinedata/deletecuisine/" + id;
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
