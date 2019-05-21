using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALFactory
{
    /// <summary>
    /// 工厂类：创建访问数据库的实例对象
    /// </summary>
    public class DALFactory
    {
        private static object GetInstance(string name)
        {
            //ILog log = LogManager.GetLogger(typeof(Factory));  //初始化日志记录器

            string configName = System.Configuration.ConfigurationManager.AppSettings["DataAccess"];
           
            if (string.IsNullOrEmpty(configName))
            {
                //log.Fatal("没有从配置文件中获取命名空间名称！");   //Fatal致命错误，优先级最高
                throw new InvalidOperationException();    //抛错，代码不会向下执行了
            }

            string className = string.Format("{0}.{1}", configName, name);  //AchieveDAL.传入的类名name

            //加载程序集
            System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(configName);
            //创建指定类型的对象实例
            return assembly.CreateInstance(className);
        }

        public static IUserDAL GetUserDAL()
        {
            IUserDAL dal = GetInstance("UserDAL") as IUserDAL;
            return dal;
        }
        public static IRoleDAL GetRoleDAL()
        {
            IRoleDAL dal = GetInstance("RoleDAL") as IRoleDAL;
            return dal;
        }
        public static IDepartmentDAL GetDepartmentDAL()
        {
            IDepartmentDAL dal = GetInstance("DepartmentDAL") as IDepartmentDAL;
            return dal;
        }
        public static IUserRoleDAL GetUserRoleDAL()
        {
            IUserRoleDAL dal = GetInstance("UserRoleDAL") as IUserRoleDAL;
            return dal;
        }
        public static IUserDepartmentDAL GetUserDepartmentDAL()
        {
            IUserDepartmentDAL dal = GetInstance("UserDepartmentDAL") as IUserDepartmentDAL;
            return dal;
        }
        public static IButtonDAL GetButtonDAL()
        {
            IButtonDAL dal = GetInstance("ButtonDAL") as IButtonDAL;
            return dal;
        }
    }
}
