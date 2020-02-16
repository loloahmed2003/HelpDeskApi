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
    public class CRM_LookupsTicketsStatusesController : ApiController
    {
        private MatrixCRMEntities db = new MatrixCRMEntities();

        // GET: api/CRM_LookupsTicketsStatuses
        public IQueryable<CRM_LookupsTicketsStatuses> GetCRM_LookupsTicketsStatuses()
        {
            return db.CRM_LookupsTicketsStatuses;
        }

        // GET: api/CRM_LookupsTicketsStatuses/5
        [ResponseType(typeof(CRM_LookupsTicketsStatuses))]
        public IHttpActionResult GetCRM_LookupsTicketsStatuses(long id)
        {
            CRM_LookupsTicketsStatuses cRM_LookupsTicketsStatuses = db.CRM_LookupsTicketsStatuses.Find(id);
            if (cRM_LookupsTicketsStatuses == null)
            {
                return NotFound();
            }

            return Ok(cRM_LookupsTicketsStatuses);
        }

        // PUT: api/CRM_LookupsTicketsStatuses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCRM_LookupsTicketsStatuses(long id, CRM_LookupsTicketsStatuses cRM_LookupsTicketsStatuses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cRM_LookupsTicketsStatuses.ID)
            {
                return BadRequest();
            }

            db.Entry(cRM_LookupsTicketsStatuses).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CRM_LookupsTicketsStatusesExists(id))
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

        // POST: api/CRM_LookupsTicketsStatuses
        [ResponseType(typeof(CRM_LookupsTicketsStatuses))]
        public IHttpActionResult PostCRM_LookupsTicketsStatuses(CRM_LookupsTicketsStatuses cRM_LookupsTicketsStatuses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CRM_LookupsTicketsStatuses.Add(cRM_LookupsTicketsStatuses);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cRM_LookupsTicketsStatuses.ID }, cRM_LookupsTicketsStatuses);
        }

        // DELETE: api/CRM_LookupsTicketsStatuses/5
        [ResponseType(typeof(CRM_LookupsTicketsStatuses))]
        public IHttpActionResult DeleteCRM_LookupsTicketsStatuses(long id)
        {
            CRM_LookupsTicketsStatuses cRM_LookupsTicketsStatuses = db.CRM_LookupsTicketsStatuses.Find(id);
            if (cRM_LookupsTicketsStatuses == null)
            {
                return NotFound();
            }

            db.CRM_LookupsTicketsStatuses.Remove(cRM_LookupsTicketsStatuses);
            db.SaveChanges();

            return Ok(cRM_LookupsTicketsStatuses);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CRM_LookupsTicketsStatusesExists(long id)
        {
            return db.CRM_LookupsTicketsStatuses.Count(e => e.ID == id) > 0;
        }
    }
}