using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;

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
                var menuData = db.Menus.Where(whereLambda);
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
