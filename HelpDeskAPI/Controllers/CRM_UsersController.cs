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
using System.Security.Cryptography;
using System.Text;

namespace HelpDeskAPI.Controllers
{
    [RoutePrefix("api/CRM_Users")]

    public class CRM_UsersController : ApiController
    {

        public string ToSHA256(string value)
        {
            SHA256 sha256 = SHA256.Create();

            byte[] hashData = sha256.ComputeHash(Encoding.Default.GetBytes(value));
            StringBuilder returnValue = new StringBuilder();

            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            return returnValue.ToString();
        }


        //private HelperMethods HashPassword = new HelperMethods();
        private MatrixCRMEntities db = new MatrixCRMEntities();
      
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login(CRM_Users user)
        {
            var userpass = ToSHA256(user.UsersPassword);
            //var userpass = HelperMethods.Encrypt(user.UsersPassword);
            var currentUser = db.CRM_Users.Where(u => u.UsersUserName == user.UsersUserName && u.UsersPassword == userpass).SingleOrDefault();

            if (user == null)
            {
                return NotFound();
            }
            if (currentUser == null)
            {
                return NotFound();
            }
            return Ok(currentUser);
        }

        [HttpPost]
        [Route("CheckUsername")]
        public IHttpActionResult CheckUsername(CRM_Users user)
        {
            try
            {
                bool usernameAlreadyExists = db.CRM_Users.Any(x => x.UsersUserName == user.UsersUserName);

                if (ModelState.IsValid && !usernameAlreadyExists)
                {

                    return Ok();
                }
                else
                {
                    return Ok("Username Taken");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }


        // GET: api/CRM_Users
        public IQueryable<CRM_Users> GetCRM_Users()
        {
            return db.CRM_Users;
        }

        // GET: api/CRM_Users/5
        [ResponseType(typeof(CRM_Users))]
        public IHttpActionResult GetCRM_Users(long id)
        {
            CRM_Users cRM_Users = db.CRM_Users.Find(id);
            if (cRM_Users == null)
            {
                return NotFound();
            }

            return Ok(cRM_Users);
        }

        // PUT: api/CRM_Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCRM_Users(long id, CRM_Users cRM_Users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cRM_Users.UsersID)
            {
                return BadRequest();
            }

            db.Entry(cRM_Users).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CRM_UsersExists(id))
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


        // POST: api/CRM_Users
        [ResponseType(typeof(CRM_Users))]
        public IHttpActionResult PostCRM_Users(CRM_Users cRM_Users)
        {
            //var data = Encoding.ASCII.GetBytes(cRM_Users.UsersPassword);
            //var sha1 = new SHA1CryptoServiceProvider();
            //var sha1data = sha1.ComputeHash(data);

            cRM_Users.UsersPassword = ToSHA256(cRM_Users.UsersPassword);
            //cRM_Users.UsersPassword = HelperMethods.Encrypt(cRM_Users.UsersPassword);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CRM_Users.Add(cRM_Users);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cRM_Users.UsersID }, cRM_Users);
        }

        // DELETE: api/CRM_Users/5
        [ResponseType(typeof(CRM_Users))]
        public IHttpActionResult DeleteCRM_Users(long id)
        {
            CRM_Users cRM_Users = db.CRM_Users.Find(id);
            if (cRM_Users == null)
            {
                return NotFound();
            }

            db.CRM_Users.Remove(cRM_Users);
            db.SaveChanges();

            return Ok(cRM_Users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CRM_UsersExists(long id)
        {
            return db.CRM_Users.Count(e => e.UsersID == id) > 0;
        }
    }
}