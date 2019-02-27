using CompaugesWebApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Web.Http;

namespace CompaugesWebApi.Controllers
{
    public class AllLeadsController : ApiController
    {
        private GTStagingNewEntities db = new GTStagingNewEntities();

        public List<IEnumerable> GetAllLeads(string SearchInput,string Deals, string leadStages  , string fromDate, string toDate , string resellerNames)
        {
            GTStagingNewEntities db = new GTStagingNewEntities();
            var results = new List<IEnumerable>();
            try
            {
                ////var vehicles = context.GetVehiclesWithRentals(DateTime.Parse(reportDate),
                //totalRentals, totalPayments);
                //Console.WriteLine("Rental Activity for {0}", reportDate);
                //Console.WriteLine("Vehicles Rented");
                fromDate = String.IsNullOrEmpty(fromDate) ? "" : fromDate;
                toDate = String.IsNullOrEmpty(toDate) ? "" : toDate;
                SearchInput = String.IsNullOrEmpty(SearchInput) ? "" : SearchInput;
                Deals = String.IsNullOrEmpty(Deals) ? "" : Deals;
                leadStages = String.IsNullOrEmpty(leadStages) ? "" : leadStages;
                Deals = String.IsNullOrEmpty(Deals) ? "" : Deals;
                resellerNames = String.IsNullOrEmpty(resellerNames) ? "" : resellerNames;
                List<SqlParameter> dbParams = new List<SqlParameter>();
                dbParams.Add(new SqlParameter("@SearchInput", SearchInput));
                dbParams.Add(new SqlParameter("@Deals", Deals));
                dbParams.Add(new SqlParameter("@leadStages", leadStages));
                dbParams.Add(new SqlParameter("@resellerNames", resellerNames));
                dbParams.Add(new SqlParameter("@fromDate", fromDate));
                dbParams.Add(new SqlParameter("@toDate", toDate));

                SqlParameter OutputParam = new SqlParameter("@retVal", SqlDbType.TinyInt, 20);
                OutputParam.Direction = ParameterDirection.Output;
                dbParams.Add(OutputParam);

                SqlParameter OutputParam1 = new SqlParameter("@retMsg", SqlDbType.VarChar, 200);
                OutputParam1.Direction = ParameterDirection.Output;
                dbParams.Add(OutputParam1);

                results = new GTStagingNewEntities()
                   .MultipleResults("[dbo].[ManageCompaugeLeads]", dbParams)
                   .With<ManageCompaugeLeads_Result>()
                   .With<LeadsSummary>()
                   .Execute();

                
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception at GetAllLeads Get" + ex.ToString());
            }


            return results;
        }

        public List<IEnumerable> GetAllSearchData()
        {
            GTStagingNewEntities db = new GTStagingNewEntities();
            var results = new List<IEnumerable>();
            try
            {
 
                List<SqlParameter> dbParams = new List<SqlParameter>();
                

                SqlParameter OutputParam = new SqlParameter("@retVal", SqlDbType.TinyInt, 20);
                OutputParam.Direction = ParameterDirection.Output;
                dbParams.Add(OutputParam);

                SqlParameter OutputParam1 = new SqlParameter("@retMsg", SqlDbType.VarChar, 200);
                OutputParam1.Direction = ParameterDirection.Output;
                dbParams.Add(OutputParam1);

                results = new GTStagingNewEntities()
                   .MultipleResults("[dbo].[GetLeadsAndResellerNames]", dbParams)
                   .With<SearchInfo.LeadStages>()
                   .With<SearchInfo.ResellerData>()
                   .Execute();


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception at GetAllLeads Get" + ex.ToString());
            }


            return results;
        }

        public IHttpActionResult Put(int mode, int LeadId, string leadName  , string ConatactName , string email , string mobile , string CompanyName , string Address , string comments , int stageID)
        {
            Logger.TraceLog("came to AllLeads put web api");
            try
            {
                Logger.TraceLog("came to AllLeads try put web api");
                List<SqlParameter> dbParams = new List<SqlParameter>();
                dbParams.Add(new SqlParameter("@mode", 4));
                dbParams.Add(new SqlParameter("@LeadId", LeadId));
                dbParams.Add(new SqlParameter("@leadName", leadName));
                dbParams.Add(new SqlParameter("@ConatactName", ConatactName));
                dbParams.Add(new SqlParameter("@email", email));
                dbParams.Add(new SqlParameter("@mobile", mobile));
                dbParams.Add(new SqlParameter("@CompanyName", CompanyName));
                dbParams.Add(new SqlParameter("@Address", Address));
                dbParams.Add(new SqlParameter("@comments", comments));
                dbParams.Add(new SqlParameter("@stageID", stageID));


                SqlParameter OutputParam = new SqlParameter("@retVal", SqlDbType.TinyInt, 20);
                OutputParam.Direction = ParameterDirection.Output;
                dbParams.Add(OutputParam);

                SqlParameter OutputParam1 = new SqlParameter("@retMsg", SqlDbType.VarChar, 200);
                OutputParam1.Direction = ParameterDirection.Output;
                dbParams.Add(OutputParam1);

                var results = new GTStagingNewEntities()
                   .MultipleResults("[dbo].[ManageCompaugeLeads]", dbParams)
                   .Execute();

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception at AllLeads put Web Api " + ex.ToString());
            }
            
           
            return Ok();
        }

        public IHttpActionResult Post(int mode, int LeadID, string leadName, string ConatactName , string companyName, string email, string mobile, int stageID)
        {
            try
            {
                List<SqlParameter> dbParams = new List<SqlParameter>();
                dbParams.Add(new SqlParameter("@mode", 2));
                dbParams.Add(new SqlParameter("@LeadID", LeadID));
                dbParams.Add(new SqlParameter("@leadName", leadName));
                dbParams.Add(new SqlParameter("@ConatactName", ConatactName));
                dbParams.Add(new SqlParameter("@companyName", companyName));
                dbParams.Add(new SqlParameter("@email", email));
                dbParams.Add(new SqlParameter("@mobile", mobile));
                dbParams.Add(new SqlParameter("@stageID", stageID));


                SqlParameter OutputParam = new SqlParameter("@retVal", SqlDbType.TinyInt, 20);
                OutputParam.Direction = ParameterDirection.Output;
                dbParams.Add(OutputParam);

                SqlParameter OutputParam1 = new SqlParameter("@retMsg", SqlDbType.VarChar, 200);
                OutputParam1.Direction = ParameterDirection.Output;
                dbParams.Add(OutputParam1);

                var results = new GTStagingNewEntities()
                   .MultipleResults("[dbo].[ManageCompaugeLeads]", dbParams)
                   .Execute();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception at AllLeads Post web api " + ex.ToString());
            }
            return Ok();
            
        }
        public IHttpActionResult Delete(int mode, int LeadID)
        {
            try
            {
                List<SqlParameter> dbParams = new List<SqlParameter>();
                dbParams.Add(new SqlParameter("@mode", 3));
                dbParams.Add(new SqlParameter("@LeadID", LeadID));



                SqlParameter OutputParam = new SqlParameter("@retVal", SqlDbType.TinyInt, 20);
                OutputParam.Direction = ParameterDirection.Output;
                dbParams.Add(OutputParam);

                SqlParameter OutputParam1 = new SqlParameter("@retMsg", SqlDbType.VarChar, 200);
                OutputParam1.Direction = ParameterDirection.Output;
                dbParams.Add(OutputParam1);

                var results = new GTStagingNewEntities()
                   .MultipleResults("[dbo].[ManageCompaugeLeads]", dbParams)
                   .Execute();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception at AllLeads Delete" + ex.ToString());
            }
            
            return Ok();
        }
    }
}
