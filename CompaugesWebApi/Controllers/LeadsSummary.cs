using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompaugesWebApi.Controllers
{
    using System;
    public class LeadsSummary
    {
        public int TotalLeadsCreated { get; set; }
        public int BusinessDemo { get; set; }
        public int BusinessLeads { get; set; }
        public int NotSatisfiedLeads { get; set; }
    }
}