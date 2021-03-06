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
    public class DepartmentDAL : IDepartmentDAL
    {
        public Department Create(Department model)
        {
            using (var db = new PermissionContext())
            {
                var department = db.Departments.Add(model);
                db.SaveChanges();
                return department;
            }
        }

        public bool Del(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var department = db.Departments.Find(id);
                db.Departments.Remove(department);

                return db.SaveChanges() > 0;
            }
        }

        public Department Find(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var department = db.Departments.Find(id);


                return department;
            }
        }

        public IEnumerable<Department> Get()
        {
            using (var db = new PermissionContext())
            {
                var department = db.Departments.OrderBy(d => d.Sort).ToList();


                return department;
            }
        }

        /// <summary>
        /// 根据用户Id获取部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Department> GetDepartmentByUserId(Guid id)
        {
            using (var db = new PermissionContext())
            {
                var departments = new List<Department>();
                var UserDepartments = db.UserDepartments.Where(d => d.UserId == id).ToList();
                if (UserDepartments.Any())
                {
                    departments = UserDepartments.Select(d => new Department
                    {
                        Id = d.Departments.Id,
                        Name = d.Departments.Name,

                    }).ToList();
                }
                return departments;
            }
        }
        public IEnumerable<Department> Get<Tkey>(Expression<Func<Department, Tkey>> orderLambda, Expression<Func<Department, bool>> whereLambda, string order, int pageSize, int pageIndex, out int totalCount)
        {
            throw new NotImplementedException();
        }

        public Department Update(Department model)
        {
            using (var db = new PermissionContext())
            {
                var department = db.Departments.Find(model.Id);
                department.Name = model.Name;
                department.Sort = model.Sort;
                department.UpdateBy = model.UpdateBy;
                department.UpdateTime = model.UpdateTime;
                db.SaveChanges();
                return department;
            }
        }
        /// <summary>
        /// 根据部门id获取用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<User> GetUserByDepartmenId(Guid[] ids)
        {
            using (var db = new PermissionContext())
            {
                var user = db.UserDepartments
                    .Where(d => ids.Any(x => x == d.DepartmentId))
                    .OrderBy(d=>d.Users.CreateBy)
                    .Select(d => d.Users)
                    .Distinct()
                    .ToList();
                return user;
            }
        }
        /// <summary>
        /// 根据部门名称获取部门数量
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public int GetDepartmentByName(string Name)
        {
            using (var db=new PermissionContext())
            {
                return db.Departments.Count(d => d.Name == Name);
            }
        }
    }
}
