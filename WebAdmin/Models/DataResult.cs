using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdmin.Models
{
    public class DataResult<T>
    {
        public int total { get; set; }
        public IEnumerable<T> rows { get; set; }
    }
}