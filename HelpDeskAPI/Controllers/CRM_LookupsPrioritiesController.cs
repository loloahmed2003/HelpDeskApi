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
using HelpDeskAPI.Models;

namespace HelpDeskAPI.Controllers
{
    public class CRM_LookupsPrioritiesController : ApiController
    {
        private MatrixCRMEntities db = new MatrixCRMEntities();

        // GET: api/CRM_LookupsPriorities
        public IQueryable<CRM_LookupsPriorities> GetCRM_LookupsPriorities()
        {
            return db.CRM_LookupsPriorities;
        }

        // GET: api/CRM_LookupsPriorities/5
        [ResponseType(typeof(CRM_LookupsPriorities))]
        public IHttpActionResult GetCRM_LookupsPriorities(long id)
        {
            CRM_LookupsPriorities cRM_LookupsPriorities = db.CRM_LookupsPriorities.Find(id);
            if (cRM_LookupsPriorities == null)
            {
                return NotFound();
            }

            return Ok(cRM_LookupsPriorities);
        }

        // PUT: api/CRM_LookupsPriorities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCRM_LookupsPriorities(long id, CRM_LookupsPriorities cRM_LookupsPriorities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cRM_LookupsPriorities.ID)
            {
                return BadRequest();
            }

            db.Entry(cRM_LookupsPriorities).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CRM_LookupsPrioritiesExists(id))
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

        // POST: api/CRM_LookupsPriorities
        [ResponseType(typeof(CRM_LookupsPriorities))]
        public IHttpActionResult PostCRM_LookupsPriorities(CRM_LookupsPriorities cRM_LookupsPriorities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CRM_LookupsPriorities.Add(cRM_LookupsPriorities);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cRM_LookupsPriorities.ID }, cRM_LookupsPriorities);
        }

        // DELETE: api/CRM_LookupsPriorities/5
        [ResponseType(typeof(CRM_LookupsPriorities))]
        public IHttpActionResult DeleteCRM_LookupsPriorities(long id)
        {
            CRM_LookupsPriorities cRM_LookupsPriorities = db.CRM_LookupsPriorities.Find(id);
            if (cRM_LookupsPriorities == null)
            {
                return NotFound();
            }

            db.CRM_LookupsPriorities.Remove(cRM_LookupsPriorities);
            db.SaveChanges();

            return Ok(cRM_LookupsPriorities);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CRM_LookupsPrioritiesExists(long id)
        {
            return db.CRM_LookupsPriorities.Count(e => e.ID == id) > 0;
        }
    }
}