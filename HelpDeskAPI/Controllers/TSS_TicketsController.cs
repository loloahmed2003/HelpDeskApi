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
using System.Web;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using AutoMapper;
using HelpDeskAPI.Models.DTO;

namespace HelpDeskAPI.Controllers
{
    [RoutePrefix("api/TSS_Tickets")]
    public class TSS_TicketsController : ApiController
    {
        private MatrixCRMEntities db = new MatrixCRMEntities();

        // GET: api/TSS_Tickets

        [Route("GetAdminOpennedTickets")]
        public IHttpActionResult GetAdminOpennedTickets()
        {
            List<TSS_TicketsDTO> result = new List<TSS_TicketsDTO>();

            IQueryable<TSS_Tickets> tickets = db.TSS_Tickets.Where(t => t.Tickets_StatusID == 1).OrderByDescending(x => x.Created).Take(100);

            foreach (var item in tickets)
            {
                result.Add(Mapper.Map<TSS_Tickets, TSS_TicketsDTO>(item));
            }
           
            //var opennedTickets = res.Where(ot => ot.Tickets_StatusID == 1).ToList();
            //var closedTickets = res.Where(ct => ct.Tickets_StatusID != 1).ToList();
            return Ok(result);
        }

        [Route("GetAdminClosedTickets")]
        public IHttpActionResult GetAdminClosedTickets()
        {

            List<TSS_TicketsDTO> result = new List<TSS_TicketsDTO>();

            IQueryable<TSS_Tickets> tickets = db.TSS_Tickets.Where(t => t.Tickets_StatusID != 1).OrderByDescending(x => x.Created).Take(100);

            foreach (var item in tickets)
            {
                result.Add(Mapper.Map<TSS_Tickets, TSS_TicketsDTO>(item));
            }
            return Ok(result);

            //var opennedTickets = res.Where(ot => ot.Tickets_StatusID == 1).ToList();
            //var closedTickets = res.Where(ct => ct.Tickets_StatusID != 1).ToList();
            //return Ok(result);
            //var res = db.TSS_Tickets.Where(t => t.Tickets_StatusID != 1).OrderByDescending(x => x.Created).Take(50).ToList();
            //var opennedTickets = res.Where(ot => ot.Tickets_StatusID == 1).ToList();
            //var closedTickets = res.Where(ct => ct.Tickets_StatusID != 1).ToList();
            //return Ok(res);
        }

        [Route("GetTSS_ClientOpennedTickets/{ClientID}")]
        public IHttpActionResult GetTSS_ClientOpennedTickets(long ClientID)
        {
            List<TSS_TicketsDTO> result = new List<TSS_TicketsDTO>();

            IQueryable<TSS_Tickets> ClientOpennedTickets = db.TSS_Tickets.Where(t => t.Tickets_Client_ID == ClientID && t.Tickets_StatusID == 1).OrderByDescending(x => x.Created);
            if (ClientOpennedTickets == null)
            {
                return NotFound();
            }
            foreach (var item in ClientOpennedTickets)
            {
                result.Add(Mapper.Map<TSS_Tickets, TSS_TicketsDTO>(item));
            }

            //var ClientOpennedTickets = db.TSS_Tickets.Where(t => t.Tickets_Client_ID == ClientID && t.Tickets_StatusID == 1).OrderByDescending(x => x.Created).ToList();
            

            return Ok(result);
        }


        [Route("GetTSS_ClientClosedTickets/{ClientID}")]
        public IHttpActionResult GetTSS_ClientClosedTickets(long ClientID)
        {
            List<TSS_TicketsDTO> result = new List<TSS_TicketsDTO>();

            IQueryable<TSS_Tickets> ClientClosedTickets = db.TSS_Tickets
                .Where(t => t.Tickets_Client_ID == ClientID && t.Tickets_StatusID != 1)
                .OrderByDescending(x => x.Created);
            if (ClientClosedTickets == null)
            {
                return NotFound();
            }
            foreach (var item in ClientClosedTickets)
            {
                result.Add(Mapper.Map<TSS_Tickets, TSS_TicketsDTO>(item));
            }
            //var ClientClosedTickets = db.TSS_Tickets.Where(t => t.Tickets_Client_ID == ClientID && t.Tickets_StatusID != 1).OrderByDescending(x => x.Created).ToList();
            //if (ClientClosedTickets == null)
            //{
            //    return NotFound();
            //}

            return Ok(result);
        }



        

        //GET: api/TSS_Tickets/5
        [ResponseType(typeof(TSS_Tickets))]
        public IHttpActionResult GetTSS_Tickets(long id)
        {
            TSS_Tickets tSS_Tickets = db.TSS_Tickets.Find(id);
            if (tSS_Tickets == null)
            {
                return NotFound();
            }

            return Ok(tSS_Tickets);
        }

        //[HttpGet]
        //[Route("PaginateTicketActions/{ticketID}/{pageNo}")]

        //public IHttpActionResult PaginateTicketActions(long ticketID, int pageNo)
        //{
        //    int pageSize = 5;
        //    //var TSS_TicketsActions = db.TSS_Tickets.Find(ticketID).TSS_TicketsActions.Skip(pageSize * pageNo).Take(pageSize).ToList();
        //    var TSS_TicketsActions = db.TSS_Tickets.FirstOrDefault(t => t.TicketsID == ticketID).TSS_TicketsActions.Skip(pageSize * pageNo).Take(pageSize).ToList();
        //    return Ok(TSS_TicketsActions);
        //}

        [HttpGet]
        [Route("PaginateTicketActions/{ticketID}/{pageNo}/{pageSize}")]
        public IHttpActionResult PaginateTicketActions(long ticketID, int pageNo = 0, int pageSize = 0)
        {
            pageSize = pageSize== 0? 5 : pageSize;
           // pageNo = pageNo == 0? 1 : pageNo;

            TSS_Tickets tSS_Tickets = db.TSS_Tickets.Find(ticketID);
            int ActionsCount = tSS_Tickets.TSS_TicketsActions.Count;

            var TicketsDTO = Mapper.Map<TSS_Tickets, TSS_TicketsDTO>(tSS_Tickets);

            //var TSS_TicketsActions = tSS_Tickets.TSS_TicketsActions.ToList();
            //var TSS_TicketsActions = tSS_Tickets.TSS_TicketsActions.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            var TSS_TicketsActions = tSS_Tickets.TSS_TicketsActions.OrderByDescending(ac => ac.CreatedDate).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            var TSS_TicketsAttachedFiles = tSS_Tickets.TSS_TicketsAttachedFiles.ToList();

            //TSS_Tickets tSS_Tickets = db.TSS_Tickets
            //                    .Where(ticket => ticket.TicketsID == id)
            //                    .Select(ticket => new TSS_Tickets
            //                    {
            //                        //OrderCount = ticket.Orders.Count(),
            //                        TSS_TicketsActions = ticket.TSS_TicketsActions.OrderByDescending(ac => ac.CreatedDate).Skip(pageSize * pageNo).Take(pageSize).ToList()
            //                    }).FirstOrDefault();


            //var tSS_Tickets = db.TSS_Tickets.First();
            //var dbEntry = db.Entry(tSS_Tickets);
            //// This is the way to use Linq-to-entities on navigation property and 
            //// load only subset of related entities
            //var entries = dbEntry.Collection(b => b.TSS_TicketsActions)
            //                     .Query()
            //                     .OrderByDescending(e => e.CreatedDate)
            //                     .Skip(pageSize * pageNo)
            //                     .Take(pageSize);


            return Ok(new { TicketsDTO, TSS_TicketsActions, ActionsCount, TSS_TicketsAttachedFiles });
        }

        //PUT: api/TSS_Tickets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTSS_Tickets(long id, TSS_Tickets tSS_Tickets)
        {
         try
           {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tSS_Tickets.TicketsID)
            {
                return BadRequest();
            }

            var existingTicket = db.TSS_Tickets.Where(c => c.TicketsID == tSS_Tickets.TicketsID).Include(cf => cf.TSS_TicketsAttachedFiles).SingleOrDefault();
            if (existingTicket != null)
            {
                // Update Category
                db.Entry(existingTicket).CurrentValues.SetValues(tSS_Tickets);


                // Delete CategoryFeatures
                foreach (var existingAttachedFiles in existingTicket.TSS_TicketsAttachedFiles.ToList())
                {
                    if (!tSS_Tickets.TSS_TicketsAttachedFiles.Any(c => c.FilesID == existingAttachedFiles.FilesID))
                        db.TSS_TicketsAttachedFiles.Remove(existingAttachedFiles);
                }
                // Update and Insert CategoryFeatures
                foreach (var newFile in tSS_Tickets.TSS_TicketsAttachedFiles)
                {
                    var existingAttachedFiles = existingTicket.TSS_TicketsAttachedFiles.Where(cf => cf.FilesID == newFile.FilesID).SingleOrDefault();
                    if (existingAttachedFiles != null)
                    {
                        //newFile.Files_TicketsID = existingTicket.TicketsID;
                        // Update catFeature
                        db.Entry(existingAttachedFiles).CurrentValues.SetValues(newFile);
                    }
                    else
                    {
                        // Insert CatFeature
                        var newAttatchedFile = new TSS_TicketsAttachedFiles
                        {
                            Files_TicketsID = existingTicket.TicketsID,
                            FilesName = newFile.FilesName
                            //...
                        };
                        existingTicket.TSS_TicketsAttachedFiles.Add(newAttatchedFile);
                    }
                }
            }
            db.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TSS_TicketsExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
            catch(Exception e)
            {
                throw e;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        //[HttpPost]
        //[Route("UploadFiles")]
        //public HttpResponseMessage UploadFiles()
        //{
        //    try
        //    {
        //        HttpResponseMessage response = new HttpResponseMessage();
        //        var httpRequest = HttpContext.Current.Request;
        //        if (httpRequest.Files.Count > 0)
        //        {
        //            var docfiles = new List<string>();
        //            foreach (string file in httpRequest.Files)
        //            {
        //                var postedFile = httpRequest.Files[file];
        //                var filePath1 = HttpContext.Current.Server.MapPath("~/FileUpload/" + postedFile.FileName);

        //                Stream strm = postedFile.InputStream;

        //                //Compressimage(strm, filePath1, postedFile.FileName);

        //            }
        //            response = Request.CreateResponse(HttpStatusCode.Created, docfiles);
        //        }
        //        else
        //        {
        //            response = Request.CreateResponse(HttpStatusCode.BadRequest);
        //        }
        //        return response;
        //    }
        //  catch(Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public static byte[] DecodeUrlBase64(string s)
        //{
        //    s = s.Replace('-', '+').Replace('_', '/').PadRight(4 * ((s.Length + 3) / 4), '=');
        //    return Convert.FromBase64String(s);
        //}

        //[NonAction]
        //public bool Upload(TSS_TicketsAttachedFiles model)
        //{
        //    string sPath = "", img = model.FilesName;
        //    sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/FileUpload/");
        //    try
        //    {
        //        if (string.IsNullOrEmpty(model.FilesName))
        //        {
        //            return false;
        //        }
        //        if (!Directory.Exists(sPath))
        //        {
        //            Directory.CreateDirectory(sPath);
        //        }

        //        string fileName = DateTime.Now.Ticks.ToString() + ".png";  //model.FilesName;

        //        sPath = Path.Combine(sPath, fileName);

        //        //string ImageData = img.Replace("data:image/png;base64,", "");
        //        string ImageData = Regex.Replace(img, "^data:image\\/[a-zA-Z]+;base64,", string.Empty);

        //        byte[] bytes = Convert.FromBase64String(ImageData);
        //        Image image;
        //        using (MemoryStream ms = new MemoryStream(bytes))
        //        {
        //            image = Image.FromStream(ms);
        //            image.Save(sPath);
        //        }
        //        model.FilesName = sPath; //fileName;
        //        image.Dispose();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}



        // POST: api/TSS_Tickets
        [ResponseType(typeof(TSS_Tickets))]
        public IHttpActionResult PostTSS_Tickets(TSS_Tickets tSS_Tickets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if (tSS_Tickets.TSS_TicketsAttachedFiles.Count > 0)
            //{
            //    if (Upload(tSS_Tickets.TSS_TicketsAttachedFiles.FirstOrDefault()))
            //    {
            //        db.TSS_Tickets.Add(tSS_Tickets);
            //        db.SaveChanges();
            //        return CreatedAtRoute("DefaultApi", new { id = tSS_Tickets.TicketsID }, tSS_Tickets);
            //    }
            //}
            //else
            //{
            db.TSS_Tickets.Add(tSS_Tickets);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = tSS_Tickets.TicketsID }, tSS_Tickets);
           // }          
           // return BadRequest();           
        }

        // DELETE: api/TSS_Tickets/5
        [ResponseType(typeof(TSS_Tickets))]
        public IHttpActionResult DeleteTSS_Tickets(long id)
        {
            TSS_Tickets tSS_Tickets = db.TSS_Tickets.Find(id);
            if (tSS_Tickets == null)
            {
                return NotFound();
            }

            db.TSS_Tickets.Remove(tSS_Tickets);
            db.SaveChanges();

            return Ok(tSS_Tickets);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TSS_TicketsExists(long id)
        {
            return db.TSS_Tickets.Count(e => e.TicketsID == id) > 0;
        }
    }
}