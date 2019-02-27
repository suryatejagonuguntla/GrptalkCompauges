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
using CompaugesWebApi.Models;

namespace CompaugesWebApi.Controllers
{
    public class CompuagesController : ApiController
    {
        private GTStagingNewEntities db = new GTStagingNewEntities();

        // GET: api/Compuages
        public IQueryable<Compuage> GetCompuages()
        {
            return db.Compuages;
        }

        // GET: api/Compuages/5
        [ResponseType(typeof(Compuage))]
        public IHttpActionResult GetCompuage(int id)
        {
            Compuage compuage = db.Compuages.Find(id);
            if (compuage == null)
            {
                return NotFound();
            }

            return Ok(compuage);
        }

        // PUT: api/Compuages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompuage(int id, Compuage compuage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != compuage.Id)
            {
                return BadRequest();
            }

            db.Entry(compuage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompuageExists(id))
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

        // POST: api/Compuages
        [ResponseType(typeof(Compuage))]
        public IHttpActionResult PostCompuage(Compuage compuage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Compuages.Add(compuage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = compuage.Id }, compuage);
        }

        // DELETE: api/Compuages/5
        [ResponseType(typeof(Compuage))]
        public IHttpActionResult DeleteCompuage(int id)
        {
            Compuage compuage = db.Compuages.Find(id);
            if (compuage == null)
            {
                return NotFound();
            }

            db.Compuages.Remove(compuage);
            db.SaveChanges();

            return Ok(compuage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompuageExists(int id)
        {
            return db.Compuages.Count(e => e.Id == id) > 0;
        }
    }
}