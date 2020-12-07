
namespace School.Dto.NavigationsMenu
{
   public class NavigationMenuAddForDto
    {
       
        public string Name { get; set; }
        public string ParentMenuId { get; set; }
        public string ParentNavigationMenu { get; set; }
        public string Area { get; set; }
        public string ControllerNam { get; set; }
        public string ActionName { get; set; }
    }
}
