using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel.Result
{
    public class MenuButtonTree:BaseMenuButton
    {
        public string state { get; set; }
        public List<MenuTree> children { get; set; }
    }
    public class MenuTree:BaseMenuButton
    {
        public string state { get; set; }
        public List<ButtonTree> children { get; set; }
    }
    public class ButtonTree: BaseMenuButton
    {
        
        public bool @checked { get; set; }
    }
    public class BaseMenuButton
    {
        public Guid id { get; set; }
        public string text { get; set; }
        public Attributess attributes { get; set; }
    }
    public class Attributess {
        public Guid menuid { get; set; }
        public Guid? buttonid { get; set; }
    }
}
