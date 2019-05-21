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
    public class UserRoleDAL : IUserRoleDAL
    {
        public UserRole Create(UserRole model)
        {
            using (var db = new PermissionContext())
            {
                var userRoles= db.UserRoles.Add(model);
                db.SaveChanges();
                return userRoles;
            }
        }

        public bool Del(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var userRole = db.UserRoles.Find(id);
                db.UserRoles.Remove(userRole);

                return db.SaveChanges() > 0;
            }
        }

        public bool DelByUserId(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var userRole = db.UserRoles.Where(d=>d.UserId==id);
                if (!userRole.Any())
                {
                    return true;
                }
                else
                {
                    if (userRole.Count() > 1)
                    {
                        db.UserRoles.RemoveRange(userRole);
                    }
                    else
                    {
                        db.UserRoles.Remove(userRole.FirstOrDefault());
                    }
                }
                

                return db.SaveChanges() > 0;
            }
        }

        public UserRole Find(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var userRole = db.UserRoles.Find(id);

                return userRole;
            }
        }

        public IEnumerable<UserRole> Get()
        {
            using (var db = new PermissionContext())
            {
                var userRoles = db.UserRoles.ToList();

                return userRoles;
            }
        }

        public IEnumerable<UserRole> Get<Tkey>(Expression<Func<UserRole, Tkey>> orderLambda, Expression<Func<UserRole, bool>> whereLambda, string order, int pageSize, int pageIndex, out int totalCount)
        {
            throw new NotImplementedException();
        }

        public UserRole Update(UserRole model)
        {
            using (var db = new PermissionContext())
            {
                var userRole = db.UserRoles.Find(model.Id);
                userRole = model;
                db.SaveChanges();

                return userRole;
            }
        }
    }
}
