//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhoneCases.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Companies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public int CompanyTypesId { get; set; }
    
        public virtual Locations Location { get; set; }
        public virtual CompanyTypes CompanyType { get; set; }
    }
}
