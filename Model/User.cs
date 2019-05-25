using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User :Base
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [DataMember(Name ="Name")]
        public string AccountName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsAble { get; set; }

        /// <summary>
        /// 是否修改密码
        /// </summary>
        public bool IsChangePwd { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Description { get; set; } 
    }
}
