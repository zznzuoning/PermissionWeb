﻿using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;

namespace Dal
{
    public class MenuButtonDAL : IMenuButtonDAL
    {
        public MenuButton Create(MenuButton model)
        {
            using (var db = new PermissionContext())
            {
                var menuButton = db.MenuButtons.Add(model);
                db.SaveChanges();
                return menuButton;
            }
        }

        public bool Del(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var menuButton = db.MenuButtons.Find(id);
                db.MenuButtons.Remove(menuButton);
                return db.SaveChanges()>0;
            }
        }

        public MenuButton Find(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var menuButton = db.MenuButtons.Find(id);
                return menuButton;
            }
        }

        public IEnumerable<MenuButton> Get()
        {
            using (var db = new PermissionContext())
            {
                var menuButton = db.MenuButtons.ToList();
                return menuButton;
            }
        }

        public IEnumerable<MenuButton> Get<Tkey>(Expression<Func<MenuButton, Tkey>> orderLambda, Expression<Func<MenuButton, bool>> whereLambda, string order, int pageSize, int pageIndex, out int totalCount)
        {
            using (var db = new PermissionContext())
            {
                var menuButtonData = db.MenuButtons.Where(whereLambda);
                totalCount = menuButtonData.Count();
                switch (order)
                {
                    case "desc":
                        menuButtonData = menuButtonData.OrderByDescending(orderLambda);
                        break;
                    case "asc":
                        menuButtonData = menuButtonData.OrderBy(orderLambda);
                        break;
                    default:
                        menuButtonData = menuButtonData.OrderBy(orderLambda);
                        break;
                }

                var menuButton = menuButtonData
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);

                return menuButton.ToList();
            }
        }

        public MenuButton Update(MenuButton model)
        {
            using (var db = new PermissionContext())
            {
                var menuButton = db.MenuButtons.Find(model.Id);
                menuButton = model;
                db.SaveChanges();
                return menuButton;
            }
        }
    }
}
