using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;

namespace Dal
{
    public class MenuButtonDAL : IMenuButtonDAL
    {
        public MenuButton Create(MenuButton model)
        {
            throw new NotImplementedException();
        }

        public bool Del(Guid id)
        {
            throw new NotImplementedException();
        }

        public MenuButton Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuButton> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuButton> Get<Tkey>(Expression<Func<MenuButton, Tkey>> orderLambda, Expression<Func<MenuButton, bool>> whereLambda, string order, int pageSize, int pageIndex, out int totalCount)
        {
            throw new NotImplementedException();
        }

        public MenuButton Update(MenuButton model)
        {
            throw new NotImplementedException();
        }
    }
}
