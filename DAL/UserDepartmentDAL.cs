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
    public class UserDepartmentDAL : IUserDepartmentDAL
    {
        public UserDepartment Create(UserDepartment model)
        {
            using (var db = new PermissionContext())
            {
                var userDepartments = db.UserDepartments.Add(model);
                db.SaveChanges();
                return userDepartments;
            }
        }

        public bool Del(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var userDepartments = db.UserDepartments.Find(id);
                db.UserDepartments.Remove(userDepartments);

                return db.SaveChanges()>0;
            }
        }

        public bool DelByUserId(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var userDepartments = db.UserDepartments.Where(d => d.UserId == id);
                if (userDepartments.Any())
                {
                    if (userDepartments.Count() > 1)
                    {
                        db.UserDepartments.RemoveRange(userDepartments);
                    }
                    else
                    {
                        db.UserDepartments.Remove(userDepartments.FirstOrDefault());
                    }
                }
                else
                {
                    return true;
                }
              
                return db.SaveChanges() > 0;
            }
        }

        public UserDepartment Find(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var userDepartments = db.UserDepartments.Find(id);
               

                return userDepartments;
            }
        }

        public IEnumerable<UserDepartment> Get()
        {
            using (var db = new PermissionContext())
            {
                var userDepartments = db.UserDepartments.ToList();


                return userDepartments;
            }
        }

        public IEnumerable<UserDepartment> Get<Tkey>(Expression<Func<UserDepartment, Tkey>> orderLambda, Expression<Func<UserDepartment, bool>> whereLambda, string order, int pageSize, int pageIndex, out int totalCount)
        {
            throw new NotImplementedException();
        }

        public UserDepartment Update(UserDepartment model)
        {
            using (var db = new PermissionContext())
            {
                var userDepartments = db.UserDepartments.Find(model.Id);
                userDepartments = model;
                db.SaveChanges();
                return userDepartments;
            }
        }
    }
}
