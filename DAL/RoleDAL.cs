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

        public IEnumerable<Role> Get<Tkey>(Expression<Func<Role, Tkey>> orderLambda, Expression<Func<Role, bool>> whereLambda,string order, int pageSize, int pageIndex, out int totalCount)
        {
            throw new NotImplementedException();
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

        public Role Update(Role model)
        {
            using (var db = new PermissionContext())
            {
                var role = db.Roles.Find(model.Id);
                role = model;
                db.SaveChanges();

                return role;
            }
        }
    }
}
