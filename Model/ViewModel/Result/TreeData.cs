using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel.Result
{
    public class TreeData
    {
        public Guid id { get; set; }
        public Guid ParentId { get; set; }
        public int Sort { get; set; }
        public string text { get; set; }
        public List<TreeData> children { get; set; }
    }
}
