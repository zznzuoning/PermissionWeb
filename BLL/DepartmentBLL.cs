using Entity;
using IDAL;
using Entity.ViewModel.Param;
using Entity.ViewModel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DepartmentBLL
    {
        IDepartmentDAL dal = DALFactory.DALFactory.GetDepartmentDAL();
        IUserDAL userDal = DALFactory.DALFactory.GetUserDAL();
        /// <summary>
        /// 根据用户Id获取部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StringData GetRoleByUserId(Guid id)
        {
            try
            {
                string departments = string.Empty;
                string ids = string.Empty;
                var departmentsInfo = dal.GetDepartmentByUserId(id);
                foreach (var department in departmentsInfo)
                {
                    ids += department.Id + ",";
                    departments += department.Name + ",";
                }
                var stringData = new StringData
                {
                    Ids=!string.IsNullOrEmpty(ids)?ids.Substring(0,ids.Length-1):ids,
                    Names=!string.IsNullOrEmpty(departments)?departments.Substring(0,departments.Length-1):departments
                };
                return stringData;
            }
            catch (Exception ex)
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }
        }

        /// <summary>
        /// 获取所有部门树
        /// </summary>
        /// <returns></returns>
        public List<TreeData> GetDepartments()
        {
            var departmentData = dal.Get().ToList();

            var departmentTree = Recursion(departmentData,Guid.Empty);
            return departmentTree;
        }
       
        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="data"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<TreeData> Recursion(List<Department> data,Guid id)
        {
            var departmentTree = new List<TreeData>();

            //获取所有顶级节点
            var topDepartmentData = data.Where(d => d.ParentId == id);
            if (topDepartmentData.Any())
            {
                foreach (var department in topDepartmentData)
                {
                    var treeData = new TreeData();
                    treeData.id = department.Id;
                    treeData.ParentId = department.ParentId;
                    treeData.Sort = department.Sort;
                    treeData.text = department.Name;
                    treeData.UpdateBy = department.UpdateBy;
                    treeData.UpdateTime = department.UpdateTime;
                    treeData.children = Recursion(data, department.Id);
                    departmentTree.Add(treeData);
                }
            }
            return departmentTree;
        }
        /// <summary>
        /// 根据部门id获取用户
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<User> GetUserByDepartmenId(Guid[] ids,int pageSize, int pageIndex, out int totalCount)
        {
            var users = dal.GetUserByDepartmenId(ids);
            totalCount = users.Count();
            var userData= users
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);
            return userData.ToList();
        }
        /// <summary>
        /// 根据部门名称查询部门是否存在
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool GetDepartmentByName(string Name)
        {
            return dal.GetDepartmentByName(Name) > 0;
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Department Create(Department model)
        {
            try
            {
                return dal.Create(model);
            }
            catch
            {
                throw new Exception("出现程序错误,请联系管理员！");
            }

        }
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Department Update(Department model)
        {
            try
            {
                return dal.Update(model);
            }
            catch
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }

        }

    }
}
