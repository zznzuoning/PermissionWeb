using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class LoginIpLog
    {
        public Guid Id { get; set; }
        public string IpAddress { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public Guid UserId { get; set; }
        public virtual User Users { get; set; }
    }
}
