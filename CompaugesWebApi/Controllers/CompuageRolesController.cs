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
    public class CompuageRolesController : ApiController
    {
        private GTStagingNewEntities db = new GTStagingNewEntities();

        // GET: api/CompuageRoles
        public IQueryable<CompuageRole> GetCompuageRoles()
        {
            return db.CompuageRoles;
        }

        // GET: api/CompuageRoles/5
        [ResponseType(typeof(CompuageRole))]
        public IHttpActionResult GetCompuageRole(byte id)
        {
            CompuageRole compuageRole = db.CompuageRoles.Find(id);
            if (compuageRole == null)
            {
                return NotFound();
            }

            return Ok(compuageRole);
        }

        // PUT: api/CompuageRoles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompuageRole(byte id, CompuageRole compuageRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != compuageRole.Id)
            {
                return BadRequest();
            }

            db.Entry(compuageRole).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompuageRoleExists(id))
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

        // POST: api/CompuageRoles
        [ResponseType(typeof(CompuageRole))]
        public IHttpActionResult PostCompuageRole(CompuageRole compuageRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CompuageRoles.Add(compuageRole);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = compuageRole.Id }, compuageRole);
        }

        // DELETE: api/CompuageRoles/5
        [ResponseType(typeof(CompuageRole))]
        public IHttpActionResult DeleteCompuageRole(byte id)
        {
            CompuageRole compuageRole = db.CompuageRoles.Find(id);
            if (compuageRole == null)
            {
                return NotFound();
            }

            db.CompuageRoles.Remove(compuageRole);
            db.SaveChanges();

            return Ok(compuageRole);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompuageRoleExists(byte id)
        {
            return db.CompuageRoles.Count(e => e.Id == id) > 0;
        }
    }
}