//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DATN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class QLCongTrinh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QLCongTrinh()
        {
            this.DanhGias = new HashSet<DanhGia>();
        }
    
        public int IDCT { get; set; }

        [Required(ErrorMessage = "This field cannot be blank!")]
        public string TenCT { get; set; }

        [Required(ErrorMessage = "This field cannot be blank!")]
        public Nullable<int> LoaiCT { get; set; }

        [Required(ErrorMessage = "This field cannot be blank!")]
        public Nullable<int> IDTinh { get; set; }

        [Required(ErrorMessage = "This field cannot be blank!")]
        public Nullable<int> IDHuyen { get; set; }

        [Required(ErrorMessage = "This field cannot be blank!")]
        public Nullable<int> IDXa { get; set; }

        [Required(ErrorMessage = "This field cannot be blank!")]
        public string LoaiHinh { get; set; }

        [Required(ErrorMessage = "This field cannot be blank!")]
        [Range(0,2000000000, ErrorMessage = "This field range cannot be smaller than 0!")]
        public Nullable<int> CSHDTK { get; set; }

        public Nullable<int> CSHDTT { get; set; }

        [Required(ErrorMessage = "This field cannot be blank!")]
        public string LoaihinhQL { get; set; }

        public Nullable<int> HoNgheo { get; set; }

        [Required(ErrorMessage = "This field cannot be blank!")]
        [Range(999, 9999, ErrorMessage = "Year range must be in between 999 and 9999!")]
        public Nullable<int> NamXayDung { get; set; }

        [Required(ErrorMessage = "This field cannot be blank!")]
        [Range(999, 9999, ErrorMessage = "Year range must be in between 999 and 9999!")]
        public Nullable<int> NamDuaVaoSD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGia> DanhGias { get; set; }
        public virtual LoaiCongTrinh LoaiCongTrinh { get; set; }
        public virtual QLHuyen QLHuyen { get; set; }
        public virtual QLTinh QLTinh { get; set; }
        public virtual QLXa QLXa { get; set; }
    }
}
