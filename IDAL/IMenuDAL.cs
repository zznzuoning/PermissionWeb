using Entity;
using Entity.ViewModel.Param;
using Entity.ViewModel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IMenuDAL : IBaseDAL<Menu>
    {
        List<MenuButtonList> GetAllMenuButtonByRoleId(Guid Id);
        List<UserMenuList> GetUserMenuByUserId(Guid Id,Guid? ParentId);
    }
}
