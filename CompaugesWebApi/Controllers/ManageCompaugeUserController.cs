using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using CompaugesWebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CompaugesWebApi.Controllers
{
    public class ManageCompaugeUserController : ApiController
    {
        GTStagingNewEntities db = new GTStagingNewEntities();

        
        
        public IHttpActionResult Post(int mode, int compId , string username , string password, string confirmPassword, string cipherKey, string Name , string Mobile ,string Email, int roleid)
        {
            Logger.TraceLog("came to Post ManageCompaugeUserController web api");

            try
            {
                Logger.TraceLog("came to Post try ManageCompaugeUserController web api");
                var identityClaims = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identityClaims.Claims;
                Logger.TraceLog("login comPid"+identityClaims.FindFirst("CompId").Value.ToString());
                var retVal = new ObjectParameter("retVal", typeof(int));
                var retMsg = new ObjectParameter("retMsg", typeof(string));
                var name = identityClaims.FindFirst("Username").Value;
                var updatedBy = identityClaims.FindFirst("CompId").Value;
                var compaugeUsers = db.ManageCompaugeUser(Convert.ToByte(mode), compId, username, password, confirmPassword , cipherKey, Name, Mobile, Email, Convert.ToInt32(updatedBy), Convert.ToByte(roleid), retVal, retMsg);
            }
            catch (Exception ex)
            {
                Logger.TraceLog("Exception at ManageCompaugeUserController Post"+ ex.ToString());
            }

            
            //foreach (var compaugeUser in compaugeUsers)
            //{
            //    string name = compaugeUser.Mobile.ToString();

            //}

            //db.ManageCompaugeUser(1, username, password, confirmPassword, Name, Mobile, Email, 1, roleid, retVal, retMsg);
            //return Convert.ToString(retMsg);
            return Ok();
        }

        public IHttpActionResult Put(int mode, int compId, string username, string password, string confirmPassword, string cipherKey, string Name, string Mobile, string Email, int roleid)
        {
            Logger.TraceLog("came to Put ManageCompaugeUserController web api");
            try
            {
                Logger.TraceLog("came to put try ManageCompaugeUserController web api");
                var identityClaims = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identityClaims.Claims;
                var retVal = new ObjectParameter("retVal", typeof(int));
                var retMsg = new ObjectParameter("retMsg", typeof(string));
                var name = identityClaims.FindFirst("Username").Value;
                var updatedBy = identityClaims.FindFirst("CompId").Value;
                var compaugeUsers = db.ManageCompaugeUser(Convert.ToByte(mode), compId, username, password, confirmPassword, cipherKey, Name, Mobile, Email, Convert.ToInt32(updatedBy), Convert.ToByte(roleid), retVal, retMsg);

            }
            catch (Exception ex)
            {
                Logger.TraceLog("Exception at ManageCompaugeUserController Put" + ex.ToString());
            }
            

            //foreach (var compaugeUser in compaugeUsers)
            //{
            //    string name = compaugeUser.Mobile.ToString();

            //}

            //db.ManageCompaugeUser(1, username, password, confirmPassword, Name, Mobile, Email, 1, roleid, retVal, retMsg);
            //return Convert.ToString(retMsg);
            return Ok();
        }

        public IHttpActionResult Delete(int mode , int compId)
        {
            Logger.TraceLog("came to Delete ManageCompaugeUserController web api");
            try
            {
                Logger.TraceLog("came to Delete try ManageCompaugeUserController web api");
                var identityClaims = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identityClaims.Claims;
                var retVal = new ObjectParameter("retVal", typeof(int));
                var retMsg = new ObjectParameter("retMsg", typeof(string));
                var name = identityClaims.FindFirst("Username").Value;
                var updatedBy = identityClaims.FindFirst("CompId").Value;
                var compaugeUsers = db.ManageCompaugeUser(Convert.ToByte(mode), compId, "", "",  "", "", "", "", "", Convert.ToInt32(updatedBy), Convert.ToByte(0), retVal, retMsg);


                //foreach (var compaugeUser in compaugeUsers)
                //{
                //    string name = compaugeUser.Mobile.ToString();

                //}

                //db.ManageCompaugeUser(1, username, password, confirmPassword, Name, Mobile, Email, 1, roleid, retVal, retMsg);
                //return Convert.ToString(retMsg);
            }
            catch (Exception ex)
            {
                Logger.TraceLog("Exception at ManageCompaugeUserController Delete" + ex.ToString());
            }
           
            return Ok();
        }
    }
}
