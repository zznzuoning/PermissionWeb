using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel.Result
{
    public class TreeData
    {
        public Guid id { get; set; }
        public Guid ParentId { get; set; }
        public int Sort { get; set; }
        public string text { get; set; }
        public List<TreeData> children { get; set; }
    }
    public class SingleTree : Singles
    {
        public List<Singles> children { get; set; }
    }
    public class Singles
    {
        public Guid id { get; set; }
        public string text { get; set; }
    }

    /// <summary>
    /// 功能导航用户菜单树
    /// </summary>
    public class UserMenutree
    {
        public Guid id { get; set; }
        public string text { get; set; }
        public string iconCls { get; set; }
        public string attributes { get; set; }
        public string state { get; set; }
        public List<UserMenutree> children { get; set; }
    }
}
