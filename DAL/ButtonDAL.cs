﻿using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;

namespace DAL
{
    public class ButtonDAL : IButtonDAL
    {
        public Button Create(Button model)
        {
            using (var db = new PermissionContext())
            {
                var button = db.Buttons.Add(model);
                db.SaveChanges();
                return button;
            }
        }

        public bool Del(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var button = db.Buttons.Find(id);
                db.Buttons.Remove(button);
                return db.SaveChanges() > 0;
            }
        }

        public Button Find(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var button = db.Buttons.Find(id);

                return button;
            }
        }

        public IEnumerable<Button> Get()
        {
            using (var db = new PermissionContext())
            {
                var button = db.Buttons.ToList();

                return button;
            }
        }

        public IEnumerable<Button> Get<Tkey>(Expression<Func<Button, Tkey>> orderLambda, Expression<Func<Button, bool>> whereLambda, string order, int pageSize, int pageIndex, out int totalCount)
        {
            using (var db = new PermissionContext())
            {
                var buttonData = db.Buttons.Where(whereLambda);
                totalCount = buttonData.Count();
                switch (order)
                {
                    case "desc":
                        buttonData = buttonData.OrderByDescending(orderLambda);
                        break;
                    case "asc":
                        buttonData = buttonData.OrderBy(orderLambda);
                        break;
                    default:
                        buttonData = buttonData.OrderBy(orderLambda);
                        break;
                }

                var button = buttonData
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);

                return button.ToList();
            }
        }

        /// <summary>
        /// 根据userid获取对应的菜单按钮权限
        /// </summary>
        /// <param name="menuCode"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Button> GetButtonByMenuCodeAndUserId(string menuCode, Guid id)
        {
            using (var db = new PermissionContext())
            {
                var button = (from ur in db.UserRoles
                              join rmb in db.RoleMenuButtons on ur.RoleId equals rmb.RoleId
                              join m in db.Menus on new { MenuId = rmb.MenuId } equals new { MenuId = m.Id }
                              join b in db.Buttons on new { ButtonId = rmb.ButtonId } equals new { ButtonId = (Guid?)b.Id }
                              where
                                ur.Users.Id == id &&
                                m.Code == menuCode
                              select b).Distinct();
                return button.OrderBy(d=>d.Sort).ToList();
            }
        }

        public List<Icons> GetIcons()
        {
            using (var db = new PermissionContext())
            {
                var icons = db.Iconss.ToList();
                return icons;
            }
        }

        public Button Update(Button model)
        {
            using (var db = new PermissionContext())
            {
                var button = db.Buttons.Find(model.Id);
                button.Name = model.Name;
                button.Code = model.Code;
                button.Icon = model.Icon;
                button.Sort = model.Sort;
                button.UpdateBy = model.UpdateBy;
                button.UpdateTime = model.UpdateTime;
                button.Description = model.Description;
                db.SaveChanges();
                return button;
            }
        }
    }
}
