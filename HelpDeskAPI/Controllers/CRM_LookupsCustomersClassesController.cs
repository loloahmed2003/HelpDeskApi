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
    public class CRM_LookupsCustomersClassesController : ApiController
    {
        private MatrixCRMEntities db = new MatrixCRMEntities();

        // GET: api/CRM_LookupsCustomersClasses
        public IQueryable<CRM_LookupsCustomersClasses> GetCRM_LookupsCustomersClasses()
        {
            return db.CRM_LookupsCustomersClasses;
        }

        // GET: api/CRM_LookupsCustomersClasses/5
        [ResponseType(typeof(CRM_LookupsCustomersClasses))]
        public IHttpActionResult GetCRM_LookupsCustomersClasses(long id)
        {
            CRM_LookupsCustomersClasses cRM_LookupsCustomersClasses = db.CRM_LookupsCustomersClasses.Find(id);
            if (cRM_LookupsCustomersClasses == null)
            {
                return NotFound();
            }

            return Ok(cRM_LookupsCustomersClasses);
        }

        // PUT: api/CRM_LookupsCustomersClasses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCRM_LookupsCustomersClasses(long id, CRM_LookupsCustomersClasses cRM_LookupsCustomersClasses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cRM_LookupsCustomersClasses.ID)
            {
                return BadRequest();
            }

            db.Entry(cRM_LookupsCustomersClasses).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CRM_LookupsCustomersClassesExists(id))
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

        // POST: api/CRM_LookupsCustomersClasses
        [ResponseType(typeof(CRM_LookupsCustomersClasses))]
        public IHttpActionResult PostCRM_LookupsCustomersClasses(CRM_LookupsCustomersClasses cRM_LookupsCustomersClasses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CRM_LookupsCustomersClasses.Add(cRM_LookupsCustomersClasses);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cRM_LookupsCustomersClasses.ID }, cRM_LookupsCustomersClasses);
        }

        // DELETE: api/CRM_LookupsCustomersClasses/5
        [ResponseType(typeof(CRM_LookupsCustomersClasses))]
        public IHttpActionResult DeleteCRM_LookupsCustomersClasses(long id)
        {
            CRM_LookupsCustomersClasses cRM_LookupsCustomersClasses = db.CRM_LookupsCustomersClasses.Find(id);
            if (cRM_LookupsCustomersClasses == null)
            {
                return NotFound();
            }

            db.CRM_LookupsCustomersClasses.Remove(cRM_LookupsCustomersClasses);
            db.SaveChanges();

            return Ok(cRM_LookupsCustomersClasses);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CRM_LookupsCustomersClassesExists(long id)
        {
            return db.CRM_LookupsCustomersClasses.Count(e => e.ID == id) > 0;
        }
    }
}