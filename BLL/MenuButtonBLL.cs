using Entity;
using IDAL;
using Model.ViewModel.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MenuButtonBLL
    {
        IMenuButtonDAL dal = DALFactory.DALFactory.GetMenuButtonDAL();
        IMenuDAL menuDal = DALFactory.DALFactory.GetMenuDAL();
        /// <summary>
        /// 添加(批量添加)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Creat(MenuButtonModel model)
        {
            try
            {
                var menuId = Guid.Parse(model.MenuIds);
                var buttonIds = model.ButtonIds.Split(',');
                var menu = menuDal.Find(menuId);
                if (menu == null)
                {
                    throw new Exception("无法识别菜单ID");
                }
                else
                {
                    var isScussce = dal.DelByMenuId(menuId);
                    if (!isScussce)
                    {
                        throw new Exception("出现程序错误,请联系管理员！");
                    }
                }
                foreach (var bid in buttonIds)
                {
                    var buttonId = Guid.Parse(bid);
                    if (buttonId != Guid.Empty)
                    {
                        var menuButton = new MenuButton
                        {
                            Id = Guid.NewGuid(),
                            MenuId = menuId,
                            ButtonId = buttonId
                        };
                        var result = dal.Create(menuButton);
                        if (result == null)
                        {
                            throw new Exception("循环添加出现程序错误,请联系管理员！");
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }
        }

        /// <summary>
        /// 根据菜单id获取按钮id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetButtonIdsByMenuId(Guid id)
        {
            var buttonIds = string.Empty;
            var buttons = dal.GetButtonByMenuId(id);
            foreach (var button in buttons)
            {
                buttonIds += button.ButtonId + ",";
            }
            return !string.IsNullOrEmpty(buttonIds) ? buttonIds.Substring(0, buttonIds.Length - 1) : buttonIds;
        }
    }
}
