//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Online_Floral_Web_App.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class sales
    {
        public int Id { get; set; }
        public Nullable<int> custid { get; set; }
        public Nullable<int> bouquetid { get; set; }
        public Nullable<System.DateTime> purchase_date { get; set; }
    
        public virtual bouquet bouquet { get; set; }
        public virtual customer customer { get; set; }
    }
}