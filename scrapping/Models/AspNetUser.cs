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
    
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            this.AspNetUserClaims = new HashSet<AspNetUserClaim>();
            this.AspNetUserLogins = new HashSet<AspNetUserLogin>();
            this.BaiViets = new HashSet<BaiViet>();
            this.InternalMessages = new HashSet<InternalMessage>();
            this.InternalMessages1 = new HashSet<InternalMessage>();
            this.RatePosts = new HashSet<RatePost>();
            this.AspNetRoles = new HashSet<AspNetRole>();
        }
    
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string Discriminator { get; set; }
        public string HoTen { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
    
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<BaiViet> BaiViets { get; set; }
        public virtual ICollection<InternalMessage> InternalMessages { get; set; }
        public virtual ICollection<InternalMessage> InternalMessages1 { get; set; }
        public virtual ICollection<RatePost> RatePosts { get; set; }
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}