//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApplication2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cases
    {
        public Cases()
        {
            this.Reconnect = false;
            this.HighPrio = false;
            this.Active = true;
        }
    
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string Info { get; set; }
        public System.DateTime StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<System.DateTime> TotalTime { get; set; }
        public Nullable<System.DateTime> CloseTime { get; set; }
        public bool Reconnect { get; set; }
        public bool HighPrio { get; set; }
        public bool Closed { get; set; }
        public bool Active { get; set; }
    
        public virtual Customers Customer { get; set; }
        public virtual Users Owner { get; set; }
    }
}
