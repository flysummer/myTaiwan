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
    
    public partial class Sign
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sign()
        {
            this.Friends = new HashSet<Friend>();
            this.Texts = new HashSet<Text>();
        }
    
        public int ID { get; set; }
        public string name { get; set; }
        public string passwords { get; set; }
        public string email { get; set; }
        public string faceUrl { get; set; }
        public string coverUrl { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Friend> Friends { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Text> Texts { get; set; }
    }
}