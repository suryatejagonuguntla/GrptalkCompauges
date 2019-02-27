using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CompaugesWebApi.Models;

namespace CompaugesWebApi.Controllers
{
    public class GetAllCompaugeUsers_ResultController : ApiController
    {
        private GTStagingNewEntities db = new GTStagingNewEntities();

        // GET: api/GetAllCompaugeUsers_Result/5
        public ObjectResult<GetAllCompaugeUsers_Result> GetGetAllCompaugeUsers_Result(int id , string userSearch, string fromDate, string toDate)
        {
            var retVal = new ObjectParameter("retVal", typeof(int));
            var retMsg = new ObjectParameter("retMsg", typeof(string));
            Logger.TraceLog("Came to GetGetAllCompaugeUsers_Result web api");
            try
            {
                Logger.TraceLog("Came to GetGetAllCompaugeUsers_Result try block");
                int iddd = id;
                string reportDate = "5/7/2013";

                ////var vehicles = context.GetVehiclesWithRentals(DateTime.Parse(reportDate),
                //totalRentals, totalPayments);
                //Console.WriteLine("Rental Activity for {0}", reportDate);
                //Console.WriteLine("Vehicles Rented");
                fromDate = String.IsNullOrEmpty(fromDate) ? "" : fromDate;
                toDate = String.IsNullOrEmpty(toDate) ? "" : toDate;
                userSearch = String.IsNullOrEmpty(userSearch) ? "" : userSearch;

                //var compaugeUsers = db.GetAllCompaugeUsers(1, fromDate, toDate, userSearch, retVal, retMsg);

                //foreach (var compaugeUser in compaugeUsers)
                //{
                //    string name = compaugeUser.Mobile.ToString();

                //}
            }
            catch(Exception ex)
            {
                Logger.ExceptionLog("Exception at GetGetAllCompaugeUsers_Result" + ex.ToString());
            }


            return db.GetAllCompaugeUsers(1, fromDate, toDate, userSearch, retVal, retMsg);
        }
        // GET: api/ManageCompauger
       
        //public string ManageCompaugeUser(byte mode , string username , string password , string confirmPassword , string Name , string Mobile , string Email , int createdby ,byte roleid  )
        //{
        //    GTStagingNewEntities db = new GTStagingNewEntities();

        //    ////var vehicles = context.GetVehiclesWithRentals(DateTime.Parse(reportDate),
        //    //totalRentals, totalPayments);
        //    //Console.WriteLine("Rental Activity for {0}", reportDate);
        //    //Console.WriteLine("Vehicles Rented");
        //    var retVal = new ObjectParameter("retVal", typeof(int));
        //    var retMsg = new ObjectParameter("retMsg", typeof(string));
        //    var compaugeUsers = db.ManageCompaugeUser(mode, username, password, confirmPassword , Name , Mobile , Email , createdby, roleid, retVal , retMsg);

        //    //foreach (var compaugeUser in compaugeUsers)
        //    //{
        //    //    string name = compaugeUser.Mobile.ToString();

        //    //}

        //     //db.ManageCompaugeUser(1, username, password, confirmPassword, Name, Mobile, Email, createdby, roleid, retVal, retMsg);
        //    return Convert.ToString(retMsg);
        //}
        //// GET: api/GetAllCompaugeUsers_Result/5
        //[ResponseType(typeof(GetAllCompaugeUsers_Result))]
        //public IHttpActionResult GetGetAllCompaugeUsers_Result(int id)
        //{
        //    GetAllCompaugeUsers_Result getAllCompaugeUsers_Result = db.GetAllCompaugeUsers_Result.Find(id);
        //    if (getAllCompaugeUsers_Result == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(getAllCompaugeUsers_Result);
        //}

        //// PUT: api/GetAllCompaugeUsers_Result/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutGetAllCompaugeUsers_Result(int id, GetAllCompaugeUsers_Result getAllCompaugeUsers_Result)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != getAllCompaugeUsers_Result.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(getAllCompaugeUsers_Result).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GetAllCompaugeUsers_ResultExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/GetAllCompaugeUsers_Result
        //[ResponseType(typeof(GetAllCompaugeUsers_Result))]
        //public IHttpActionResult PostGetAllCompaugeUsers_Result(GetAllCompaugeUsers_Result getAllCompaugeUsers_Result)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.GetAllCompaugeUsers_Result.Add(getAllCompaugeUsers_Result);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = getAllCompaugeUsers_Result.Id }, getAllCompaugeUsers_Result);
        //}

        //// DELETE: api/GetAllCompaugeUsers_Result/5
        //[ResponseType(typeof(GetAllCompaugeUsers_Result))]
        //public IHttpActionResult DeleteGetAllCompaugeUsers_Result(int id)
        //{
        //    GetAllCompaugeUsers_Result getAllCompaugeUsers_Result = db.GetAllCompaugeUsers_Result.Find(id);
        //    if (getAllCompaugeUsers_Result == null)
        //    {
        //        return NotFound();
        //    }

        //    db.GetAllCompaugeUsers_Result.Remove(getAllCompaugeUsers_Result);
        //    db.SaveChanges();

        //    return Ok(getAllCompaugeUsers_Result);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool GetAllCompaugeUsers_ResultExists(int id)
        //{
        //    return db.GetAllCompaugeUsers_Result.Count(e => e.Id == id) > 0;
        //}
    }
}