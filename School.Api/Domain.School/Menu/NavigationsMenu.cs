using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Menu
{
  public  class NavigationsMenu
    {
     public   NavigationsMenu()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentMenuId { get; set; }
        public string ParentNavigationMenu { get; set; }
        public string Area { get; set; }
        public string ControllerNam { get; set; }
        public string ActionName { get; set; }
    }
}
