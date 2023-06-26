using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PP_RestaurantReview.Models;
using System.Diagnostics;

namespace PP_RestaurantReview.Controllers
{
    public class ReviewDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ReviewData/ListReviews
        [HttpGet]
        [ResponseType(typeof(ReviewDto))]
        public IHttpActionResult ListReviews()
        {
            List<Review> Reviews = db.Reviews.ToList();
            List<ReviewDto> ReviewDtos = new List<ReviewDto>();

            Reviews.ForEach(rv => ReviewDtos.Add(new ReviewDto()
            {
                ReviewId = rv.ReviewId,
                ReviewDate = rv.ReviewDate,
                ReviewRating = rv.ReviewRating,
                ReviewComment = rv.ReviewComment,
                RestaurantId = rv.ReviewId,
                RestaurantName = rv.Restaurant.RestaurantName,
                UserId = rv.UserId
            }));
            return Ok(ReviewDtos);
        }

        //Gathers information about all reviews related to a particular restaurant
        //GET: api/ReviewData/ListReviewsForRestaurant/3
        [HttpGet]
        [ResponseType(typeof(ReviewDto))]
        public IHttpActionResult ListReviewsForRestaurant(int id)
        {
            List<Review> Reviews = db.Reviews.Where(rv=>rv.RestaurantId==id).ToList();
            List<ReviewDto> ReviewDtos = new List<ReviewDto>();

            Reviews.ForEach(rv => ReviewDtos.Add(new ReviewDto()
            {
                ReviewId = rv.ReviewId,
                ReviewDate = rv.ReviewDate,
                ReviewRating = rv.ReviewRating,
                ReviewComment = rv.ReviewComment,
                RestaurantId = rv.ReviewId,
                RestaurantName = rv.Restaurant.RestaurantName,
                UserId = rv.UserId
            }));
            return Ok(ReviewDtos);
        }

        // GET: api/ReviewData/FindReview/5
        [ResponseType(typeof(ReviewDto))]
        [HttpGet]
        public IHttpActionResult FindReview(int id)
        {
            Review Review = db.Reviews.Find(id);
            ReviewDto ReviewDto = new ReviewDto()
            {
                ReviewId = Review.ReviewId,
                ReviewDate = Review.ReviewDate,
                ReviewRating = Review.ReviewRating,
                ReviewComment = Review.ReviewComment,
                RestaurantId = Review.ReviewId,
                RestaurantName = Review.Restaurant.RestaurantName,
                UserId = Review.UserId
            };

            if (Review == null)
            {
                return NotFound();
            }

            return Ok(ReviewDto);
        }

        // POST: api/ReviewData/UpdateReview/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateReview(int id, Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != review.ReviewId)
            {
                return BadRequest();
            }

            db.Entry(review).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ReviewData/AddReview
        [ResponseType(typeof(Review))]
        [HttpPost]
        public IHttpActionResult AddReview(Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reviews.Add(review);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = review.ReviewId }, review);
        }

        // POST: api/ReviewData/DeleteReview/5
        [ResponseType(typeof(Review))]
        [HttpPost]
        public IHttpActionResult DeleteReview(int id)
        {
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }

            db.Reviews.Remove(review);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReviewExists(int id)
        {
            return db.Reviews.Count(e => e.ReviewId == id) > 0;
        }
    }
}