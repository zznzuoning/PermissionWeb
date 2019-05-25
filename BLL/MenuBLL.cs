using Entity;
using IDAL;
using Entity.ViewModel.Result;
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
                    menuTreeData.children = Recursion(data, item.Id);
                    treeData.Add(menuTreeData);
                }
            }
            return treeData;
        }
        /// <summary>
        /// 根据用户id获取对应的菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public List<UserMenutree> GetUserMenuByUserId(Guid Id, Guid? ParentId)
        {
            var userMenu = dal.GetUserMenuByUserId(Id, ParentId);
            var tree = userMenu.Select(d => new UserMenutree
            {
                id = d.MenuId,
                text = d.MenuName,
                iconCls = d.Icon,
                attributes = d.LinkAddress,
                state = dal.Get().Any(x => x.ParentId == d.MenuId) ? "closed" : "open"
            }).ToList();
            return tree;
        }

        /// <summary>
        /// 根据角色id获取所有菜单按钮
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<MenuButtonTree> GetAllMenuButtonByRoleId(Guid Id)
        {

            var list = new List<MenuButtonTree>();
            var data = dal.GetAllMenuButtonByRoleId(Id);

            var distinctMenuData = data.GroupBy(d => d.MenuId).Select(d => new DistinctMenu
            {
                Id = d.Key,
                Name = d.Max(x => x.MenuName),
                ParentId = d.Max(x => x.ParentId)
            }).ToList();
            var topData = data.Where(d => d.ParentId == null);
            foreach (var item in topData)
            {
                string stateStr = "closed";
                var menuButton = new MenuButtonTree();
                var attributes = new Attributess();
                menuButton.id = item.MenuId;
                menuButton.state = stateStr;
                menuButton.text = item.MenuName;
                attributes.buttonid = item.ButtonId;
                attributes.menuid = item.MenuId;
                menuButton.attributes = attributes;
                menuButton.children = RecursionMenuButton(data, distinctMenuData, item.MenuId, Id, stateStr);
                list.Add(menuButton);
            }
            return list;
        }
        private List<MenuTree> RecursionMenuButton(List<MenuButtonList> data, List<DistinctMenu> menuData, Guid menuId, Guid roleId, string stateStr)
        {
            var list = new List<MenuTree>();
            var childMenu = menuData.Where(d => d.ParentId == menuId);
            foreach (var menu in childMenu)
            {
                var buttonList = new List<ButtonTree>();
                var menuButton = new MenuTree();
                var attributes = new Attributess();
                menuButton.id = menu.Id;
                menuButton.state = stateStr;
                menuButton.text = menu.Name;
                attributes.buttonid = null;
                attributes.menuid = menu.Id;
                menuButton.attributes = attributes;
                var buttonTree = data.Where(d => d.MenuId == menu.Id && d.ButtonId.HasValue);
                if (buttonTree.Any())
                {
                    foreach (var button in buttonTree)
                    {
                        var buttons = new ButtonTree();
                        var buttonAttribute = new Attributess();
                        buttons.id = roleId;
                        buttons.text = button.ButtonName;
                        buttons.@checked = button.Checked;
                        buttons.attributes = buttonAttribute;
                        buttonAttribute.buttonid = button.ButtonId;
                        buttonAttribute.menuid = button.MenuId;
                        buttonList.Add(buttons);
                    }
                }
                menuButton.children = buttonList;
                list.Add(menuButton);
            }
            return list;
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
