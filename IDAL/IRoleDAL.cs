﻿using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IRoleDAL : IBaseDAL<Role>
    {
        List<Role> GetRoleByUserId(Guid id);
    }
}
