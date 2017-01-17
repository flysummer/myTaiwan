//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace myTaiwanTest.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Text
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Text()
        {
            this.Images = new HashSet<Image>();
            this.Pictures = new HashSet<Picture>();
        }
    
        public int txtID { get; set; }
        public string txtTitle { get; set; }
        public System.DateTime txtCreateTime { get; set; }
        public System.DateTime txtUpdateTime { get; set; }
        public int userID { get; set; }
        public int location { get; set; }
        public string locationDescription { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual Sign Sign { get; set; }
        public virtual County County { get; set; }
    }
}