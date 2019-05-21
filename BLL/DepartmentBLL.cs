using Entity;
using IDAL;
using Model.ViewModel.Param;
using Model.ViewModel.Result;
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
                    treeData.children = Recursion(data, department.Id);
                    departmentTree.Add(treeData);
                }
            }
            return departmentTree;
        }
    }
}
