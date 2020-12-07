using AutoMapper;
using Infra.School.Data.AutoMapper;
using Infra.School.Data.Contract;
using Infra.School.Data.Db;
using School.Domain.Menu;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infra.School.Data.MenusAssingmentRepository
{
    public class NavigationsMenuRepository : INavigationsMenuRespository
    {
        private readonly IMapper mapper;
        public   NavigationsMenuRepository()
        {
            mapper = DbAutoMapperProfiles.Mappers(); ;
        }
        public async Task<List<NavigationsMenu>> AddMenu(NavigationsMenu menus)
        {
            using (var entity = new SchoolErp())
            {

                var menutobeAdd = new NavigationMenu() {Id=menus.Id, Name=menus.Name,
                ParentMenuId=menus.ParentMenuId,ParentNavigationMenu=menus.ParentNavigationMenu,
                Area=menus.Area, ControllerName=menus.ControllerNam,ActionName=menus.ActionName};
                entity.NavigationMenus.Add(menutobeAdd);
                var result = await entity.SaveChangesAsync(); 
                if (result >= 1)
                {
                  var nav= await entity.NavigationMenus.Where(x=>x.ParentMenuId==null).ToListAsync();
                    if(nav!=null)
                    return mapper.Map<List<NavigationsMenu>>(nav);
                }

            };
            return null;
        }

        public async Task<List<NavigationMenuResponse>> GetMenu(string userName)
        {
            using (var entity = new SchoolErp())
            {

                List<NavigationMenuResponse> list = new List<NavigationMenuResponse>();
                var query =await ( from u in entity.Users
                            join ur in entity.UserRoles on u.UniqueId equals ur.UniqueId
                            join mr in entity.RoleMenuPermissions on ur.roleId equals mr.RoleId
                            join nm in entity.NavigationMenus on mr.NavigationMenuId equals nm.Id
                            join np in entity.NavigationMenus on nm.Id equals np.ParentMenuId
                            where u.userId == userName
                            select new
                            {
                                Name = np.Name,
                                Parent = nm.Name,
                                ActionName = np.ActionName,
                                ControllerName = np.ControllerName,
                                UserName = u.userId,
                                iconName=np.iconName
                            }).ToListAsync();
                 foreach(var q in query)
                {
                    NavigationMenuResponse obj = new NavigationMenuResponse();
                    NavingationChild childobj = new NavingationChild();
                    obj.ParentName = q.Parent;
                    obj.ActionName = q.ActionName;
                    obj.ControllerName = q.ControllerName;
                    obj.Name = q.Name;
                    obj.IconName = q.iconName;
                    list.Add(obj);
                }
                return list;

            };
           
        }
    }
}
