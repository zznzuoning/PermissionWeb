using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel.Result
{
    public class RoleList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public string Description { get; set; }
    }
}
