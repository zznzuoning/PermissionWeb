using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WenAdmin.Models.Param.User
{
    public class UserSearch
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string  LogName{ get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 是否使用
        /// </summary>
        public string IsUse { get; set; }
        /// <summary>
        /// 是否改密
        /// </summary>
        public string IsEditPass { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int Rows { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 排序方式（asc,desc）
        /// </summary>
        public string Order { get; set; }
    }
}