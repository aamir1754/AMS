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
    
    public partial class ams_items
    {
        public int itm_key { get; set; }
        public string itm_serial_no { get; set; }
        public string itm_location { get; set; }
        public string itm_status { get; set; }
        public Nullable<System.DateTime> itm_order_date { get; set; }
        public Nullable<System.DateTime> itm_complete_date { get; set; }
        public Nullable<int> itm_added_by { get; set; }
        public string itm_notes { get; set; }
    
        public virtual ams_users ams_users { get; set; }
    }
}
