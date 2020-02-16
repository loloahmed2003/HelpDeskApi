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
    public class TSS_ClientsController : ApiController
    {
        private MatrixCRMEntities db = new MatrixCRMEntities();

        // GET: api/TSS_Clients
        public IQueryable<TSS_Clients> GetTSS_Clients()
        {
            var res = db.TSS_Clients.OrderByDescending(x => x.Created).Take(50);

            return res;
        }

        // GET: api/TSS_Clients/5
        [ResponseType(typeof(TSS_Clients))]
        public IHttpActionResult GetTSS_Clients(long id)
        {
            TSS_Clients tSS_Clients = db.TSS_Clients.Find(id);
            if (tSS_Clients == null)
            {
                return NotFound();
            }

            return Ok(tSS_Clients);
        }
        

        // PUT: api/TSS_Clients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTSS_Clients(long id, TSS_Clients tSS_Clients)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tSS_Clients.ClientsID)
            {
                return BadRequest();
            }

            db.Entry(tSS_Clients).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TSS_ClientsExists(id))
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

        // POST: api/TSS_Clients
        [ResponseType(typeof(TSS_Clients))]
        public IHttpActionResult PostTSS_Clients(TSS_Clients tSS_Clients)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TSS_Clients.Add(tSS_Clients);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TSS_ClientsExists(tSS_Clients.ClientsID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tSS_Clients.ClientsID }, tSS_Clients);
        }

        // DELETE: api/TSS_Clients/5
        [ResponseType(typeof(TSS_Clients))]
        public IHttpActionResult DeleteTSS_Clients(long id)
        {
            TSS_Clients tSS_Clients = db.TSS_Clients.Find(id);
            if (tSS_Clients == null)
            {
                return NotFound();
            }

            db.TSS_Clients.Remove(tSS_Clients);
            db.SaveChanges();

            return Ok(tSS_Clients);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TSS_ClientsExists(long id)
        {
            return db.TSS_Clients.Count(e => e.ClientsID == id) > 0;
        }
    }
}