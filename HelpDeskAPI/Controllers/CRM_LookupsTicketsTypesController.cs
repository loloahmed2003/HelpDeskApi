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
    public class CRM_LookupsTicketsTypesController : ApiController
    {
        private MatrixCRMEntities db = new MatrixCRMEntities();

        // GET: api/CRM_LookupsTicketsTypes
        public IQueryable<CRM_LookupsTicketsTypes> GetCRM_LookupsTicketsTypes()
        {
            return db.CRM_LookupsTicketsTypes;
        }

        // GET: api/CRM_LookupsTicketsTypes/5
        [ResponseType(typeof(CRM_LookupsTicketsTypes))]
        public IHttpActionResult GetCRM_LookupsTicketsTypes(long id)
        {
            CRM_LookupsTicketsTypes cRM_LookupsTicketsTypes = db.CRM_LookupsTicketsTypes.Find(id);
            if (cRM_LookupsTicketsTypes == null)
            {
                return NotFound();
            }

            return Ok(cRM_LookupsTicketsTypes);
        }

        // PUT: api/CRM_LookupsTicketsTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCRM_LookupsTicketsTypes(long id, CRM_LookupsTicketsTypes cRM_LookupsTicketsTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cRM_LookupsTicketsTypes.ID)
            {
                return BadRequest();
            }

            db.Entry(cRM_LookupsTicketsTypes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CRM_LookupsTicketsTypesExists(id))
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

        // POST: api/CRM_LookupsTicketsTypes
        [ResponseType(typeof(CRM_LookupsTicketsTypes))]
        public IHttpActionResult PostCRM_LookupsTicketsTypes(CRM_LookupsTicketsTypes cRM_LookupsTicketsTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CRM_LookupsTicketsTypes.Add(cRM_LookupsTicketsTypes);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cRM_LookupsTicketsTypes.ID }, cRM_LookupsTicketsTypes);
        }

        // DELETE: api/CRM_LookupsTicketsTypes/5
        [ResponseType(typeof(CRM_LookupsTicketsTypes))]
        public IHttpActionResult DeleteCRM_LookupsTicketsTypes(long id)
        {
            CRM_LookupsTicketsTypes cRM_LookupsTicketsTypes = db.CRM_LookupsTicketsTypes.Find(id);
            if (cRM_LookupsTicketsTypes == null)
            {
                return NotFound();
            }

            db.CRM_LookupsTicketsTypes.Remove(cRM_LookupsTicketsTypes);
            db.SaveChanges();

            return Ok(cRM_LookupsTicketsTypes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CRM_LookupsTicketsTypesExists(long id)
        {
            return db.CRM_LookupsTicketsTypes.Count(e => e.ID == id) > 0;
        }
    }
}