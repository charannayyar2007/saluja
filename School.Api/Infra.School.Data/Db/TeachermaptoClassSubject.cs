//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infra.School.Data.Db
{
    using System;
    using System.Collections.Generic;
    
    public partial class TeachermaptoClassSubject
    {
        public int id { get; set; }
        public Nullable<int> classId { get; set; }
        public Nullable<int> subjectid { get; set; }
        public Nullable<int> sectionid { get; set; }
        public string enrollmentCode { get; set; }
        public Nullable<System.DateTime> createdOn { get; set; }
        public System.Guid UniqueId { get; set; }
    }
}
