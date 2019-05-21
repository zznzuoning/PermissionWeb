﻿using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IDepartmentDAL :IBaseDAL<Department>
    {
        List<Department> GetDepartmentByUserId(Guid id);
    }
}
