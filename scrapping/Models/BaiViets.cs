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
    
    public partial class BaiViets
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiViets()
        {
            this.RatePost = new HashSet<RatePost>();
        }
    
        public int ID { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string NguoiDang { get; set; }
        public System.DateTime NgayCapNhat { get; set; }
        public int Trangthai { get; set; }
        public string Tabs { get; set; }
        public string linkImage { get; set; }
        public Nullable<int> SoLanTruyCap { get; set; }
        public string ChuyenMuc { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RatePost> RatePost { get; set; }
    }
}