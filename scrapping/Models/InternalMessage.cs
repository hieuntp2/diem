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
    
    public partial class InternalMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Mesage { get; set; }
        public System.DateTime DateCreate { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
    }
}