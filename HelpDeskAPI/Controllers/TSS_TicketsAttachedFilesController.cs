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
using System.IO;

namespace HelpDeskAPI.Controllers
{
    [RoutePrefix("api/TSS_TicketsAttachedFiles")]

    public class TSS_TicketsAttachedFilesController : ApiController
    {
        private MatrixCRMEntities db = new MatrixCRMEntities();


        [HttpPost]
        [Route("UploadFiles")]

        public string UploadFiles()
        {
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/UploadImages/");
            var request = System.Web.HttpContext.Current.Request;
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            //var remark = request["remark"].ToString();
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];
                if (hpf.ContentLength > 0)
                {
                    string FileName = (Path.GetFileName(hpf.FileName));
                    if (!File.Exists(sPath + FileName))
                    {
                        // SAVE THE FILES IN THE FOLDER.  
                        hpf.SaveAs(sPath + FileName);
                        TSS_TicketsAttachedFiles obj = new TSS_TicketsAttachedFiles();
                        obj.FilesName = FileName;
                        db.TSS_TicketsAttachedFiles.Add(obj);
                        db.SaveChanges();
                    }
                }
            }
            return "Upload Failed";
        }


        // GET: api/TSS_TicketsAttachedFiles
        public IQueryable<TSS_TicketsAttachedFiles> GetTSS_TicketsAttachedFiles()
        {
            return db.TSS_TicketsAttachedFiles;
        }

        // GET: api/TSS_TicketsAttachedFiles/5
        [ResponseType(typeof(TSS_TicketsAttachedFiles))]
        public IHttpActionResult GetTSS_TicketsAttachedFiles(long id)
        {
            TSS_TicketsAttachedFiles tSS_TicketsAttachedFiles = db.TSS_TicketsAttachedFiles.Find(id);
            if (tSS_TicketsAttachedFiles == null)
            {
                return NotFound();
            }

            return Ok(tSS_TicketsAttachedFiles);
        }

        // PUT: api/TSS_TicketsAttachedFiles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTSS_TicketsAttachedFiles(long id, TSS_TicketsAttachedFiles tSS_TicketsAttachedFiles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tSS_TicketsAttachedFiles.FilesID)
            {
                return BadRequest();
            }

            db.Entry(tSS_TicketsAttachedFiles).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TSS_TicketsAttachedFilesExists(id))
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

        // POST: api/TSS_TicketsAttachedFiles
        [ResponseType(typeof(TSS_TicketsAttachedFiles))]
        public IHttpActionResult PostTSS_TicketsAttachedFiles(TSS_TicketsAttachedFiles tSS_TicketsAttachedFiles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TSS_TicketsAttachedFiles.Add(tSS_TicketsAttachedFiles);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tSS_TicketsAttachedFiles.FilesID }, tSS_TicketsAttachedFiles);
        }

        // DELETE: api/TSS_TicketsAttachedFiles/5
        [ResponseType(typeof(TSS_TicketsAttachedFiles))]
        public IHttpActionResult DeleteTSS_TicketsAttachedFiles(long id)
        {
            TSS_TicketsAttachedFiles tSS_TicketsAttachedFiles = db.TSS_TicketsAttachedFiles.Find(id);
            if (tSS_TicketsAttachedFiles == null)
            {
                return NotFound();
            }

            db.TSS_TicketsAttachedFiles.Remove(tSS_TicketsAttachedFiles);
            db.SaveChanges();

            return Ok(tSS_TicketsAttachedFiles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TSS_TicketsAttachedFilesExists(long id)
        {
            return db.TSS_TicketsAttachedFiles.Count(e => e.FilesID == id) > 0;
        }
    }
}