using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using CompaugesWebApi.Models;
using Microsoft.Owin.Security;

namespace CompaugesWebApi
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {

        //public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        //{
        //    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
        //    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "*" });
        //    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "*" });

        //    return base.MatchEndpoint(context);
        //}

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            Logger.TraceLog("login validation start");
            //var userStore = new UserStore<CompaugeUser>(new ApplicationDbContext());
            //var manager = new UserManager<CompaugeUser>(userStore);
            //var user = await manager.FindAsync(context.UserName, context.Password);
            //if (user != null)
            //{
            //    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //    identity.AddClaim(new Claim("Username", user.UserName));
            //    identity.AddClaim(new Claim("Email", user.Email));
            //    identity.AddClaim(new Claim("FirstName", user.FirstName));
            //    identity.AddClaim(new Claim("LastName", user.LastName));
            //    identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));
            //    context.Validated(identity);
            //}
            //else
            //{

            //    context.SetError("invalid_grant", "Provided username and password is incorrect");
            //    context.Rejected();
            //}
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "*" });
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "*" });
            try {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                using (var db = new GTStagingNewEntities())
                {
                    if (db != null)
                    {
                        var empl = db.CompuageRoles.ToList();
                        var user = db.Compuages.ToList();
                        var CompDetail = db.CompuageUserDetails.ToList();
                        if (user != null)
                        {

                            if (user.Where(u => u.UserName == context.UserName && u.Password == context.Password).Count() > 0)
                            {
                                Logger.TraceLog("UserName and password matched");
                                identity.AddClaim(new Claim("Age", "16"));
                                identity.AddClaim(new Claim("Username", user.Where(u => u.UserName == context.UserName && u.Password == context.Password).FirstOrDefault().Name));
                                identity.AddClaim(new Claim("Email", user.Where(u => u.UserName == context.UserName && u.Password == context.Password).FirstOrDefault().Email));
                                identity.AddClaim(new Claim("FirstName", user.Where(u => u.UserName == context.UserName && u.Password == context.Password).FirstOrDefault().Mobile));
                                identity.AddClaim(new Claim("LastName", user.Where(u => u.UserName == context.UserName && u.Password == context.Password).FirstOrDefault().Name));
                                identity.AddClaim(new Claim("CompId", user.Where(u => u.UserName == context.UserName && u.Password == context.Password).FirstOrDefault().Id.ToString()));
                                identity.AddClaim(new Claim("role", user.Where(u => u.UserName == context.UserName && u.Password == context.Password).FirstOrDefault().Id.ToString()));
                                identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));

                                Logger.TraceLog("Username " + user.Where(u => u.UserName == context.UserName && u.Password == context.Password).FirstOrDefault().Name);
                                Logger.TraceLog("CompId " + user.Where(u => u.UserName == context.UserName && u.Password == context.Password).FirstOrDefault().Id.ToString());
                                var props = new AuthenticationProperties(new Dictionary<string, string>
                                {
                                    {
                                        "userdisplayname", context.UserName
                                    },
                                    {
                                         "role", user.Where(u => u.UserName == context.UserName && u.Password == context.Password).FirstOrDefault().RoleId.ToString()
                                    }
                                 });
                                

                                var ticket = new AuthenticationTicket(identity, props);
                                context.Validated(ticket);
                            }
                            else
                            {
                                context.SetError("invalid_grant", "Provided username and password is incorrect");
                                context.Rejected();
                            }
                        }
                    }
                    else
                    {
                        context.SetError("invalid_grant", "Provided username and password is incorrect");
                        context.Rejected();
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.TraceLog("Exception at GrantResourceOwnerCredentials" + ex.ToString());
                context.SetError("invalid_grant", "Some thing wen wrong");
                context.Rejected();
            }

            return;
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}
 
