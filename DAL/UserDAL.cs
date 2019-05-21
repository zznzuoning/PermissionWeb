using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace DAL
{
    /// <summary>
    /// 用户（SQL Server数据库实现）
    /// </summary>
    public class UserDAL : IUserDAL
    {
        public User Create(User model)
        {
            using (var db = new PermissionContext())
            {
                var user = db.Users.Add(model);
                db.SaveChanges();
                return user;
            }
        }

        public bool Del(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);

                return db.SaveChanges() > 0;
            }
        }

        public User Find(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var user = db.Users.Find(id);

                return user;
            }
        }

        public IEnumerable<User> Get()
        {
            using (var db = new PermissionContext())
            {
                var user = db.Users.ToList();

                return user;
            }
        }

        public IEnumerable<User> Get<Tkey>(Expression<Func<User, Tkey>> orderLambda, Expression<Func<User, bool>> whereLambda,string order, int pageSize, int pageIndex, out int totalCount)
        {
            using (var db = new PermissionContext())
            {
                var userData = db.Users.Where(whereLambda);
                totalCount = userData.Count();
                switch (order)
                {
                    case "desc":
                        userData= userData.OrderByDescending(orderLambda);
                        break;
                    case "asc":
                        userData = userData.OrderBy(orderLambda);
                        break;
                    default:
                        userData = userData.OrderBy(orderLambda);
                        break;
                }

                var user = userData
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);

                return user.ToList();
            }
        }

        public User Update(User model)
        {
            using (var db = new PermissionContext())
            {
                var user = db.Users.Find(model.Id); ;
                user.RealName = model.RealName;
                user.MobilePhone = model.MobilePhone;
                user.Email = model.Email;
                user.IsAble = model.IsAble;
                user.IsChangePwd = model.IsChangePwd;
                user.UpdateBy = model.UpdateBy;
                user.UpdateTime = model.UpdateTime;
                db.SaveChanges();
                return user;

            }
        }
    }
}
