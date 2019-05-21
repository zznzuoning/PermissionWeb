using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel.Result
{
    public class ButtonList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public int Sort { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public string UpdateBy { get; set; }
        public string Description { get; set; }
    }
}
