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
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using HelpDeskAPI.Models.DTO;

namespace HelpDeskAPI.Controllers
{
    [RoutePrefix("api/TSS_TicketsActions")]

    public class TSS_TicketsActionsController : ApiController
    {
        private MatrixCRMEntities db = new MatrixCRMEntities();

        // GET: api/TSS_TicketsActions
        public IQueryable<TSS_TicketsActions> GetTSS_TicketsActions()
        {
            return db.TSS_TicketsActions;
        }

        // GET: api/TSS_TicketsActions/5
        [ResponseType(typeof(TSS_TicketsActions))]
        public IHttpActionResult GetTSS_TicketsActions(long id)
        {
            TSS_TicketsActions tSS_TicketsActions = db.TSS_TicketsActions.Find(id);
            if (tSS_TicketsActions == null)
            {
                return NotFound();
            }

            return Ok(tSS_TicketsActions);
        }


        // PUT: api/TSS_TicketsActions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTSS_TicketsActions(long id, TSS_TicketsActions tSS_TicketsActions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tSS_TicketsActions.ActionID)
            {
                return BadRequest();
            }

            //db.Entry(tSS_TicketsActions).State = EntityState.Modified;

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!TSS_TicketsActionsExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}


            Regex regex = new Regex(@"^data\:(?<type>image\/(png|tiff|jpg|gif|jpeg));base64,(?<data>[A-Z0-9\+\/\=]+)$",
                                        RegexOptions.Compiled |
                                        RegexOptions.ExplicitCapture |
                                        RegexOptions.IgnoreCase);
            Match match = regex.Match(tSS_TicketsActions.msg);

            if (!match.Success)
            {
                var commandText = string.Format(@"UPDATE TSS_TicketsActions SET IsSeen = {0}, msg = N'{1}' WHERE ActionID = {2}",
                                                tSS_TicketsActions.IsSeen.Value == true ? 1 : 0,
                                                tSS_TicketsActions.msg,
                                                id);
                db.Database.ExecuteSqlCommand(commandText);
            }
            else
            {
                var commandText = string.Format(@"UPDATE TSS_TicketsActions SET IsSeen = {0}, msg = '{1}' WHERE ActionID = {2}",
                                               tSS_TicketsActions.IsSeen.Value == true ? 1 : 0,
                                                tSS_TicketsActions.msg,
                                                id);
                db.Database.ExecuteSqlCommand(commandText);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //[HttpPost]
        //[Route("CheckMsgTime")]
        //public IHttpActionResult CheckMsgTime()
        //{
        //    try
        //    {
        //        var lastAction = db.TSS_TicketsActions.OrderByDescending(u => u.ActionID).FirstOrDefault();

        //        //var lastAction = db.TSS_TicketsActions.GroupBy(m => m.CreatedByID).OrderByDescending(m => m.ActionID).FirstOrDefault();

        //        DateTime now = DateTime.Now;
        //        DateTime msgTime = (DateTime)lastAction.TimeCreated;
        //        TimeSpan value = now.Subtract(msgTime);

        //        if (value.Minutes > 5)
        //        {
        //            return Ok(new { });
        //        }

        //        else
        //        {
        //            return  Ok(new { message = "You should wait about 5 minutes" } );
        //        }
        //    }
        //  catch(Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        private long GetUserID(long id)
        {
            var user = db.TSS_Clients.Where(c => c.ClientsID == id).Select(c => c.CRM_Users).FirstOrDefault();
            return user == null ? id : user.UsersID;
        }

        //[HttpGet]
        //[Route("CheckMsgTime/{id}")]
        //public IHttpActionResult CheckMsgTime(long id)
        //{
        //    try
        //    {
        //        //var userID = GetUserID(id);

        //        //var lastAction = db.TSS_TicketsActions.OrderByDescending(u => u.ActionID).Where(u => u.UserID == userID).FirstOrDefault();
        //        var lastAction = db.TSS_TicketsActions.OrderByDescending(u => u.ActionID).Where(u => u.UserID == id).FirstOrDefault();
        //        DateTime now = DateTime.Now;
        //        DateTime msgTime = (DateTime)lastAction.CreatedDate;
        //        TimeSpan value = now.Subtract(msgTime);

        //        if (value.Minutes > 5)
        //        {
        //            return Ok();
        //        }

        //        else
        //        {
        //            return Ok("You should wait about 5 minutes");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}


        [Route("GetActionsByTicketID/{id}")]
        public IHttpActionResult GetActionsByTicketID(long id)
        {
            var TicketActions = db.TSS_TicketsActions.Where(t => t.TicketID == id).ToList();
            if (TicketActions == null)
            {
                return NotFound();
            }

            return Ok(TicketActions);
        }

        // POST: api/TSS_TicketsActions

        [HttpGet]
        [Route("CheckMsgTime/{id}/{ticketID}")]
        public IHttpActionResult CheckMsgTime(long id, long ticketID)
        {
            try
            {
                //var userID = GetUserID(id);
                //var lastAction = db.TSS_TicketsActions.OrderByDescending(u => u.ActionID).Where(u => u.UserID == userID).FirstOrDefault();
                var lastAction = db.TSS_TicketsActions.OrderByDescending(u => u.ActionID).Where(u => u.UserID == id && u.TicketID == ticketID).FirstOrDefault();
                DateTime now = DateTime.Now;
                DateTime msgTime = (DateTime)lastAction.CreatedDate;
                TimeSpan value = now.Subtract(msgTime);

                if (value.Minutes > 2)
                {
                    return Ok();
                }

                else
                {
                    return Ok("You should wait about 2 minutes");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        [ResponseType(typeof(TSS_TicketsActions))]
        public IHttpActionResult PostTSS_TicketsActions(TSS_TicketsActionsDTO tSS_TicketsActions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           // string ImageData = Regex.Replace(tSS_TicketsActions.msg, "^data:image\\/[a-zA-Z]+;base64,", string.Empty);

            Regex regex = new Regex(@"^data\:(?<type>image\/(png|tiff|jpg|gif|jpeg));base64,(?<data>[A-Z0-9\+\/\=]+)$", 
                                        RegexOptions.Compiled | 
                                        RegexOptions.ExplicitCapture | 
                                        RegexOptions.IgnoreCase);
            Match match = regex.Match(tSS_TicketsActions.msg);

            if (!match.Success)
            {
                var commandText = string.Format(@"INSERT TSS_TicketsActions VALUES ({0}, N'{1}', {2}, {3}, '{4}')",
                                                tSS_TicketsActions.TicketID,
                                                tSS_TicketsActions.msg,
                                                tSS_TicketsActions.UserID,
                                                tSS_TicketsActions.IsSeen.Value == true ? 1 : 0,
                                                tSS_TicketsActions.CreatedDate);
                db.Database.ExecuteSqlCommand(commandText);
            }
            else
            {
                var commandText = string.Format(@"INSERT TSS_TicketsActions VALUES ({0}, '{1}', {2}, {3}, '{4}')",
                                                tSS_TicketsActions.TicketID,
                                                tSS_TicketsActions.msg,
                                                tSS_TicketsActions. UserID,
                                                tSS_TicketsActions.IsSeen.Value == true ? 1 : 0,
                                                tSS_TicketsActions.CreatedDate);
                db.Database.ExecuteSqlCommand(commandText);
            }
            
            // db.TSS_TicketsActions.Add(tSS_TicketsActions);
            // db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tSS_TicketsActions.ActionID }, tSS_TicketsActions);
        }

        // DELETE: api/TSS_TicketsActions/5
        [ResponseType(typeof(TSS_TicketsActions))]
        public IHttpActionResult DeleteTSS_TicketsActions(long id)
        {
            TSS_TicketsActions tSS_TicketsActions = db.TSS_TicketsActions.Find(id);
            if (tSS_TicketsActions == null)
            {
                return NotFound();
            }

            db.TSS_TicketsActions.Remove(tSS_TicketsActions);
            db.SaveChanges();

            return Ok(tSS_TicketsActions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TSS_TicketsActionsExists(long id)
        {
            return db.TSS_TicketsActions.Count(e => e.ActionID == id) > 0;
        }
    }
}