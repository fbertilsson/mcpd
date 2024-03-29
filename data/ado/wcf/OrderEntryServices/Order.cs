//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrderEntryServices
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public Order()
        {
            this.Order_Details = new HashSet<Order_Detail>();
        }
    
        public int Order_ID { get; set; }
        public string Customer_ID { get; set; }
        public Nullable<int> Employee_ID { get; set; }
        public string Ship_Name { get; set; }
        public string Ship_Address { get; set; }
        public string Ship_City { get; set; }
        public string Ship_Region { get; set; }
        public string Ship_Postal_Code { get; set; }
        public string Ship_Country { get; set; }
        public Nullable<int> Ship_Via { get; set; }
        public Nullable<System.DateTime> Order_Date { get; set; }
        public Nullable<System.DateTime> Required_Date { get; set; }
        public Nullable<System.DateTime> Shipped_Date { get; set; }
        public Nullable<decimal> Freight { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Order_Detail> Order_Details { get; set; }
    }
}
