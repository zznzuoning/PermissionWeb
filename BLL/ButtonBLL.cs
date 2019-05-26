using Entity;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ButtonBLL
    {
        IButtonDAL dal = DALFactory.DALFactory.GetButtonDAL();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">button实体</param>
        /// <returns></returns>
        public Button Create(Button model)
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
                    bool isSuccess= dal.Del(gid);
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
        /// <summary>
        /// 根据id查询按钮
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Button Find(Guid id)
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
        /// 获取所有按钮
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Button> Get()
        {
            return dal.Get();
        }

        /// <summary>
        /// 获取所有按钮(分页)
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="orderLambda">排序</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="order">排序方式(desc,asc)</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="totalCount">总条数</param>
        /// <returns></returns>
        public IEnumerable<Button> Get<Tkey>(Expression<Func<Button, Tkey>> orderLambda, Expression<Func<Button, bool>> whereLambda, string order, int pageSize, int pageIndex, out int totalCount)
        {
            return dal.Get(orderLambda,whereLambda,order,pageSize,pageIndex,out totalCount);
        }

        /// <summary>
        /// 获取所有图标
        /// </summary>
        /// <returns></returns>
        public List<Icons> GetIcons()
        {
            return dal.GetIcons();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">button实体</param>
        /// <returns></returns>
        public Button Update(Button model)
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
        /// 根据userid获取对应的菜单按钮权限
        /// </summary>
        /// <param name="menuCode"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Button> GetButtonByMenuCodeAndUserId(string menuCode,Guid id)
        {
            try
            {
                return dal.GetButtonByMenuCodeAndUserId(menuCode,id).Where(d=>!d.Code.Contains("search")).ToList();
            }
            catch (Exception)
            {

                throw new Exception("出现程序错误,请联系管理员！");
            }
        }
    }
}
