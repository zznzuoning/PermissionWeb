using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel.Result
{
    public class MenuTreeData : BaseTree
    {
       
        public List<ChildrenTreeData> children { get; set; }
    }
    public class ChildrenTreeData : BaseTree
    {
        public Attributes attributes { get; set; }
        public List<ChildrenTreeData> children { get; set; }
    }
    public class BaseTree
    {
        public Guid id { get; set; }
        public string text { get; set; }
        public string iconCls { get; set; }
    }
    public class Attributes
    {
        public string url { get; set; }
    }
}
