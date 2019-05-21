using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;

namespace DAL
{
    public class MenuDAL : IMenuDAL
    {
        public Menu Create(Menu model)
        {
            throw new NotImplementedException();
        }

        public bool Del(Guid id)
        {
            throw new NotImplementedException();
        }

        public Menu Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Menu> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Menu> Get<Tkey>(Expression<Func<Menu, Tkey>> orderLambda, Expression<Func<Menu, bool>> whereLambda, string order, int pageSize, int pageIndex, out int totalCount)
        {
            throw new NotImplementedException();
        }

        public Menu Update(Menu model)
        {
            throw new NotImplementedException();
        }
    }
}
