//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMS.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ams_websites
    {
        public int web_key { get; set; }
        public Nullable<int> web_user_key { get; set; }
        public string web_title { get; set; }
        public string web_information { get; set; }
        public string web_url { get; set; }
    
        public virtual ams_users ams_users { get; set; }
    }
}
