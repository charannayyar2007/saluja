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
    
    public partial class NavigationMenu
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentMenuId { get; set; }
        public string ParentNavigationMenu { get; set; }
        public string Area { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public Nullable<bool> IsExternal { get; set; }
        public string ExternalUrl { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<bool> Permitted { get; set; }
        public Nullable<bool> Visible { get; set; }
        public string iconName { get; set; }
    }
}
