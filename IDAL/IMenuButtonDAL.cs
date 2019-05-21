using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IMenuButtonDAL : IBaseDAL<MenuButton>
    {
        bool DelByMenuId(Guid id);
        List<MenuButton> GetButtonByMenuId(Guid id);
    }
}
