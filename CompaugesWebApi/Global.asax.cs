﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CompaugesWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Logger.Initialize();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }
        protected void Application_BeginRequest()
        {

            //if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            //{
            //    //These headers are handling the "pre-flight" OPTIONS call sent by the browser
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "http://localhost:4723");
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
            //    //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Accepts, Content-Type, Origin, X-My-Header");
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "*");
            //    HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "60");
            //    HttpContext.Current.Response.End();
            //}

        }
    }
}
