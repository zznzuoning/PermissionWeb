using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IButtonDAL : IBaseDAL<Button>
    {
        List<Icons> GetIcons();
        List<Button> GetButtonByMenuCodeAndUserId(string menuCode,Guid id);
    }
}
