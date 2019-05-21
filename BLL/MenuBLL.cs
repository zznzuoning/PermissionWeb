using Entity;
using IDAL;
using Model.ViewModel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public class MenuBLL
    {
        IMenuDAL dal = DALFactory.DALFactory.GetMenuDAL();

        /// <summary>
        /// 获取所有菜单(分页)
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="orderLambda">排序</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="order">排序方式(asc,desc)</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="totalCount">总条数</param>
        /// <returns></returns>
        public IEnumerable<Menu> Get<Tkey>(Expression<Func<Menu, Tkey>> orderLambda, Expression<Func<Menu, bool>> whereLambda, string order, int pageSize, int pageIndex, out int totalCount)
        {
            return dal.Get(orderLambda, whereLambda, order, pageSize, pageIndex, out totalCount);
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public List<MenuTreeData> GetMenuTreeList()
        {
            var treeData = new List<MenuTreeData>();
            var menuData = dal.Get().ToList();
            var topMenuData = menuData.Where(d => d.ParentId == null);
            if (topMenuData.Any())
            {
                foreach (var item in topMenuData)
                {
                    var menuTreeData = new MenuTreeData();
                    menuTreeData.id = item.Id;
                    menuTreeData.text = item.Name;
                    menuTreeData.iconCls = item.Icon;
                    menuTreeData.children = Recursion(menuData, item.Id);
                    treeData.Add(menuTreeData);
                }
            }
            return treeData;
        }
        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="data"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<ChildrenTreeData> Recursion(List<Menu> data, Guid? id)
        {
            var treeData = new List<ChildrenTreeData>();
            var menuData = data.Where(d => d.ParentId == id);
            if (menuData.Any())
            {
                foreach (var item in menuData)
                {
                    var menuTreeData = new ChildrenTreeData();
                    var attributes = new Attributes();
                    attributes.url = item.LinkAddress;
                    menuTreeData.id = item.Id;
                    menuTreeData.text = item.Name;
                    menuTreeData.iconCls = item.Icon;
                    menuTreeData.attributes = attributes;
                    menuTreeData.children = Recursion(data,item.Id);
                    treeData.Add(menuTreeData);
                }
            }
            return treeData;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Menu Creat(Menu model)
        {
            try
            {
                return dal.Create(model);
            }
            catch (Exception)
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Menu Update(Menu model)
        {
            try
            {
                return dal.Update(model);
            }
            catch (Exception)
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }
        }

        /// <summary>
        /// 根据id获取菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Menu Find(Guid id)
        {
            try
            {
                return dal.Find(id);
            }
            catch (Exception)
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }
        }
        /// <summary>
        /// 删除(支持批量删除)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Del(string id)
        {
            try
            {
                var ids = id.Split(',');
                foreach (var item in ids)
                {
                    var gid = Guid.Parse(item);
                    bool isSuccess = dal.Del(gid);
                    if (!isSuccess)
                    {
                        throw new Exception("循环删除出现程序错误,请联系管理员！");
                    }
                }
                return true;
            }
            catch (Exception)
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }
        }
    }
}
