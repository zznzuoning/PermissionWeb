using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel.Param
{
    public class GetUserByDepartmentId :BaseSearch
    {
        public Guid[] Ids { get; set; }
    }
}
