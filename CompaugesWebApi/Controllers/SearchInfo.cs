using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompaugesWebApi.Controllers
{
    public class SearchInfo 
    {
        public class LeadStages
        {
            public int Id { get; set; }
            public string Stage { get; set; }
        }

        public class ResellerData
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class ImageInfo
        {
            public string ProfileImage { get; set; }
            public string ImageName { get; set; }

        }
    }
}