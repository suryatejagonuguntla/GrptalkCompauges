//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompaugesWebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdWordLead
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Nullable<byte> Type { get; set; }
        public Nullable<System.DateTime> InsertedAt { get; set; }
        public Nullable<byte> LeadType { get; set; }
        public Nullable<byte> IsAssigned { get; set; }
        public string FullName { get; set; }
        public string Comments { get; set; }
        public string Source { get; set; }
        public string Gclid { get; set; }
        public string QueryString { get; set; }
        public string UrlReferer { get; set; }
        public string PricingPlan { get; set; }
        public Nullable<int> CompId { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public string Company { get; set; }
        public Nullable<byte> LeadStage { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string LeadName { get; set; }
    }
}
