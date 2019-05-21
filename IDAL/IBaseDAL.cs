using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IBaseDAL<T> where T : class, new()
    {
        T Create(T model);
        T Update(T model);
        bool Del(Guid id);
        T Find(Guid id);
        IEnumerable<T> Get();
        IEnumerable<T> Get<Tkey>(Expression<Func<T,Tkey>> orderLambda, Expression<Func<T, bool>> whereLambda,string order,int pageSize, int pageIndex,out int totalCount);

    }
}
