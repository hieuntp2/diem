//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace scrapping.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserComments
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Href { get; set; }
        public string Comment { get; set; }
        public Nullable<int> Rate { get; set; }
        public Nullable<System.DateTime> ReportDate { get; set; }
    }
}