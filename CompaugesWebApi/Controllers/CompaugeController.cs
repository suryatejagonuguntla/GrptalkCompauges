using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CompaugesWebApi.Models;
using System.Security.Claims;

namespace CompaugesWebApi.Controllers
{
    public class CompaugeController : ApiController
    {
        [HttpGet]
        [Route("api/GetUserClaims")]
        public CompaugeModel GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            CompaugeModel model = new CompaugeModel()
            {
                UserName = identityClaims.FindFirst("Username").Value,
                Email = identityClaims.FindFirst("Email").Value,
                FirstName = identityClaims.FindFirst("FirstName").Value,
                LastName = identityClaims.FindFirst("LastName").Value,
                LoggedOn = identityClaims.FindFirst("LoggedOn").Value,
                CompID = identityClaims.FindFirst("CompId").Value
            };
            return model;
        }
    }
}
