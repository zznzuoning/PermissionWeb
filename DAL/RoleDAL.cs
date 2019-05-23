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
    public class RoleDAL : IRoleDAL
    {
        public Role Create(Role model)
        {
            using (var db = new PermissionContext())
            {
                var role = db.Roles.Add(model);
                db.SaveChanges();
                return role;
            }
        }

        public bool Del(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var role = db.Roles.Find(id);
                db.Roles.Remove(role);

                return db.SaveChanges() > 0;
            }
        }

        public Role Find(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var role = db.Roles.Find(id);

                return role;
            }
        }

        public IEnumerable<Role> Get()
        {
            using (var db = new PermissionContext())
            {
                var role = db.Roles.ToList();

                return role;
            }
        }

        public IEnumerable<Role> Get<Tkey>(Expression<Func<Role, Tkey>> orderLambda, Expression<Func<Role, bool>> whereLambda, string order, int pageSize, int pageIndex, out int totalCount)
        {
            using (var db = new PermissionContext())
            {
                var roleData = db.Roles.Where(whereLambda);
                totalCount = roleData.Count();
                switch (order)
                {
                    case "desc":
                        roleData = roleData.OrderByDescending(orderLambda);
                        break;
                    case "asc":
                        roleData = roleData.OrderBy(orderLambda);
                        break;
                    default:
                        roleData = roleData.OrderBy(orderLambda);
                        break;
                }

                var role = roleData
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);

                return role.ToList();
            }
        }

        public List<Role> GetRoleByUserId(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var roles = new List<Role>();
                var userRole = db.UserRoles.Where(d => d.UserId == id).ToList();
                if (userRole.Any())
                {
                    roles = userRole.Select(d => new Role
                    {
                        Id = d.Roles.Id,
                        RoleName = d.Roles.RoleName,

                    }).ToList();
                }

                return roles;
            }
        }

        public List<User> GetUserByRoleId<Tkey>(Guid id, Expression<Func<User, Tkey>> orderLambda, string order, int pageSize, int pageIndex, out int totalCount)
        {
            using (var db = new PermissionContext())
            {
                var userList = new List<User>();
                var userRoles = db.UserRoles.Where(d => d.RoleId == id);
                if (userRoles.Any())
                {
                    var users = userRoles.Select(d => d.Users);

                    switch (order)
                    {
                        case "desc":
                            users = users.OrderByDescending(orderLambda);
                            break;
                        case "asc":
                            users = users.OrderBy(orderLambda);
                            break;
                        default:
                            users = users.OrderBy(orderLambda);
                            break;
                    }

                    userList = users
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize).ToList();
                }
                totalCount = userList.Count();
                return userList;
            }
        }

        public Role Update(Role model)
        {
            using (var db = new PermissionContext())
            {
                var role = db.Roles.Find(model.Id);
                role.RoleName = model.RoleName;
                role.Description = model.Description;
                role.UpdateBy = model.UpdateBy;
                role.UpdateTime = model.UpdateTime;
                db.SaveChanges();

                return role;
            }
        }
    }
}
