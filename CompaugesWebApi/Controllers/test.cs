using CompaugesWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompaugesWebApi.Controllers
{
    public class test : Controller
    {
        // GET: test
        public ActionResult Index()
        {
            GTStagingNewEntities db = new GTStagingNewEntities();

            string reportDate = "5/7/2013";
            var retVal = new ObjectParameter("retVal", typeof(int));
            var retMsg = new ObjectParameter("retMsg", typeof(string));
            ////var vehicles = context.GetVehiclesWithRentals(DateTime.Parse(reportDate),
            //totalRentals, totalPayments);
            //Console.WriteLine("Rental Activity for {0}", reportDate);
            //Console.WriteLine("Vehicles Rented");
            return View(db.GetAllCompaugeUsers(1, reportDate , reportDate ,  "" , retVal  , retMsg));
        }

        //// GET: Home
        //public ActionResult Index()
        //{
        //    NorthwindEntities entities = new NorthwindEntities();
        //    return View(entities.SearchCustomers(""));
        //}

        //[HttpPost]
        //public ActionResult Index(string customerName)
        //{
        //    NorthwindEntities entities = new NorthwindEntities();
        //    return View(entities.SearchCustomers(customerName));
        //}
    }
}