using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;
using Entity.ViewModel.Result;

namespace DAL
{
    public class MenuDAL : IMenuDAL
    {
        public Menu Create(Menu model)
        {
            using (var db = new PermissionContext())
            {
                var menu = db.Menus.Add(model);
                db.SaveChanges();
                return menu;
            }
        }

        public bool Del(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var menu = db.Menus.Find(id);
                db.Menus.Remove(menu);

                return db.SaveChanges() > 0;
            }
        }

        public Menu Find(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var menu = db.Menus.Find(id);


                return menu;
            }
        }

        public IEnumerable<Menu> Get()
        {
            using (var db = new PermissionContext())
            {
                var menu = db.Menus.ToList();


                return menu;
            }
        }

        public IEnumerable<Menu> Get<Tkey>(Expression<Func<Menu, Tkey>> orderLambda, Expression<Func<Menu, bool>> whereLambda, string order, int pageSize, int pageIndex, out int totalCount)
        {
            using (var db = new PermissionContext())
            {
                var menuData = db.Menus.Where(whereLambda).ToList().AsQueryable();
                totalCount = menuData.Count();
                switch (order)
                {
                    case "desc":
                        menuData = menuData.OrderByDescending(orderLambda);
                        break;
                    case "asc":
                        menuData = menuData.OrderBy(orderLambda);
                        break;
                    default:
                        menuData = menuData.OrderBy(orderLambda);
                        break;
                }

                var menu = menuData
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);

                return menu.ToList();
            }
        }

        /// <summary>
        ///根据角色id 获取所有菜单按钮
        /// </summary>
        /// <param name="Id">角色Id</param>
        /// <returns></returns>
        public List<MenuButtonList> GetAllMenuButtonByRoleId(Guid Id)
        {
            using (var db = new PermissionContext())
            {

                var data = from m in db.Menus
                           join mb in db.MenuButtons on new { Id = m.Id } equals new { Id = mb.MenuId } into mb_join
                           from mb in mb_join.DefaultIfEmpty()
                           join b in db.Buttons on new { ButtonId = mb.ButtonId } equals new { ButtonId = b.Id } into b_join
                           from b in b_join.DefaultIfEmpty()
                           join rmb in db.RoleMenuButtons
                                 on new { mb.MenuId, ButtonId = mb.ButtonId, RoleId = Id }
                             equals new { rmb.MenuId, ButtonId = rmb.ButtonId.Value, RoleId = rmb.RoleId } into rmb_join
                           from rmb in rmb_join.DefaultIfEmpty()
                           orderby
                             m.Sort,
                             b.Sort
                           select new MenuButtonList
                           {
                               MenuId = m.Id,
                               MenuName = m.Name,
                               ParentId = m.ParentId,
                               MenuIcon = m.Icon,
                               ButtonId = mb.ButtonId,
                               ButtonName = b.Name,
                               ButtonIcon = b.Icon,
                               RoleId = rmb.RoleId,
                               Checked =
                             rmb.ButtonId == null ? false : true
                           };
                return data.ToList();
            }
        }
        /// <summary>
        /// 根据用户id获取对应的菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public List<UserMenuList> GetUserMenuByUserId(Guid Id, Guid? ParentId)
        {
            using (var db = new PermissionContext())
            {
                var menu = (from ur in db.UserRoles
                            join rmb in db.RoleMenuButtons on ur.RoleId equals rmb.RoleId into rmb_join
                            from rmb in rmb_join.DefaultIfEmpty()
                            join m in db.Menus on new { MenuId = rmb.MenuId } equals new { MenuId = m.Id } into m_join
                            from m in m_join.DefaultIfEmpty()
                            where
                              ur.Users.Id == Id &&
                              m.ParentId == ParentId
                            orderby
                              m.Sort
                            select new UserMenuList
                            {
                                MenuId = m.Id,
                                MenuName = m.Name,
                                Icon = m.Icon,
                                LinkAddress = m.LinkAddress
                            }).Distinct();
                return menu.ToList();
            }
        }

        public Menu Update(Menu model)
        {
            using (var db = new PermissionContext())
            {
                var menu = db.Menus.Find(model.Id); ;
                menu.Name = model.Name;
                menu.ParentId = model.ParentId;
                menu.LinkAddress = model.LinkAddress;
                menu.Icon = model.Icon;
                menu.Code = model.Code;
                menu.Sort = model.Sort;
                menu.UpdateBy = model.UpdateBy;
                menu.UpdateTime = model.UpdateTime;
                db.SaveChanges();
                return menu;

            }
        }
    }
}
