using Entity;
using Model.ViewModel.Param;
using Model.ViewModel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IMenuDAL : IBaseDAL<Menu>
    {
        List<MenuButtonList> GetAllMenuButton(Guid Id);
    }
}
