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

namespace PP_RestaurantReview.Controllers
{
    public class CuisineDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CuisineData/ListCuisines
        [HttpGet]
        [ResponseType(typeof(CuisineDto))]
        public IEnumerable<CuisineDto> ListCuisines()
        {
            List<Cuisine> Cuisines = db.Cuisines.ToList();
            List<CuisineDto> CuisineDtos = new List<CuisineDto>();

            Cuisines.ForEach(c => CuisineDtos.Add(new CuisineDto()
            {
                CuisineId = c.CuisineId,
                CuisineType = c.CuisineType

            }));

            return CuisineDtos;
        }

        // GET: api/CuisineData/FindCuisine/5
        [ResponseType(typeof(CuisineDto))]
        [HttpGet]
        public IHttpActionResult FindCuisine(int id)
        {
            Cuisine Cuisine = db.Cuisines.Find(id);
            CuisineDto CuisineDto = new CuisineDto()
            {
                CuisineId = Cuisine.CuisineId,
                CuisineType = Cuisine.CuisineType
            };

            if (Cuisine == null)
            {
                return NotFound();
            }

            return Ok(CuisineDto);
        }

        // POST: api/CuisineData/UpdateCuisine/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateCuisine(int id, Cuisine cuisine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuisine.CuisineId)
            {
                return BadRequest();
            }

            db.Entry(cuisine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuisineExists(id))
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

        // POST: api/CuisineData/AddCuisine
        [ResponseType(typeof(Cuisine))]
        [HttpPost]
        public IHttpActionResult AddCuisine(Cuisine cuisine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cuisines.Add(cuisine);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cuisine.CuisineId }, cuisine);
        }

        // POST: api/CuisineData/DeleteCuisine/5
        [ResponseType(typeof(Cuisine))]
        [HttpPost]
        public IHttpActionResult DeleteCuisine(int id)
        {
            Cuisine cuisine = db.Cuisines.Find(id);
            if (cuisine == null)
            {
                return NotFound();
            }

            db.Cuisines.Remove(cuisine);
            db.SaveChanges();

            return Ok(cuisine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CuisineExists(int id)
        {
            return db.Cuisines.Count(e => e.CuisineId == id) > 0;
        }
    }
}