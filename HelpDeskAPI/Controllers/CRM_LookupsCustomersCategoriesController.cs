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
    public class CRM_LookupsCustomersCategoriesController : ApiController
    {
        private MatrixCRMEntities db = new MatrixCRMEntities();

        // GET: api/CRM_LookupsCustomersCategories
        public IQueryable<CRM_LookupsCustomersCategories> GetCRM_LookupsCustomersCategories()
        {
            return db.CRM_LookupsCustomersCategories;
        }

        // GET: api/CRM_LookupsCustomersCategories/5
        [ResponseType(typeof(CRM_LookupsCustomersCategories))]
        public IHttpActionResult GetCRM_LookupsCustomersCategories(long id)
        {
            CRM_LookupsCustomersCategories cRM_LookupsCustomersCategories = db.CRM_LookupsCustomersCategories.Find(id);
            if (cRM_LookupsCustomersCategories == null)
            {
                return NotFound();
            }

            return Ok(cRM_LookupsCustomersCategories);
        }

        // PUT: api/CRM_LookupsCustomersCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCRM_LookupsCustomersCategories(long id, CRM_LookupsCustomersCategories cRM_LookupsCustomersCategories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cRM_LookupsCustomersCategories.ID)
            {
                return BadRequest();
            }

            db.Entry(cRM_LookupsCustomersCategories).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CRM_LookupsCustomersCategoriesExists(id))
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

        // POST: api/CRM_LookupsCustomersCategories
        [ResponseType(typeof(CRM_LookupsCustomersCategories))]
        public IHttpActionResult PostCRM_LookupsCustomersCategories(CRM_LookupsCustomersCategories cRM_LookupsCustomersCategories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CRM_LookupsCustomersCategories.Add(cRM_LookupsCustomersCategories);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cRM_LookupsCustomersCategories.ID }, cRM_LookupsCustomersCategories);
        }

        // DELETE: api/CRM_LookupsCustomersCategories/5
        [ResponseType(typeof(CRM_LookupsCustomersCategories))]
        public IHttpActionResult DeleteCRM_LookupsCustomersCategories(long id)
        {
            CRM_LookupsCustomersCategories cRM_LookupsCustomersCategories = db.CRM_LookupsCustomersCategories.Find(id);
            if (cRM_LookupsCustomersCategories == null)
            {
                return NotFound();
            }

            db.CRM_LookupsCustomersCategories.Remove(cRM_LookupsCustomersCategories);
            db.SaveChanges();

            return Ok(cRM_LookupsCustomersCategories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CRM_LookupsCustomersCategoriesExists(long id)
        {
            return db.CRM_LookupsCustomersCategories.Count(e => e.ID == id) > 0;
        }
    }
}