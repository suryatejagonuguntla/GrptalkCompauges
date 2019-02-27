﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace CompaugesWebApi
{

    public class Logger
    {
        public static ILog defaultLogger = null;
        public static ILog traceLogger = null;
        public static void Initialize()
        {
            log4net.GlobalContext.Properties["LogName"] = DateTime.Now.ToString("yyyyMMdd");

            defaultLogger = log4net.LogManager.GetLogger("DefaultLogger");
            traceLogger = log4net.LogManager.GetLogger("TraceLogger");

            log4net.Config.XmlConfigurator.Configure();
        }
        public static void ExceptionLog(string _Exception)
        {
            defaultLogger.Error("Exception : " + _Exception.ToString());

            if (HttpContext.Current.Request.HttpMethod.ToString().ToUpper() == "GET")
            {
                defaultLogger.Info("Input Data : " + HttpContext.Current.Request.RawUrl.ToString());
            }
            else
            {
                System.IO.StreamReader SReader = new System.IO.StreamReader(HttpContext.Current.Request.InputStream);
                defaultLogger.Info("Input Data : " + SReader.ReadToEnd());
            }

        }
        public static void TraceLog(string _Input)
        {
            traceLogger.Info(" Trace : " + _Input.ToString());
        }
    }
}