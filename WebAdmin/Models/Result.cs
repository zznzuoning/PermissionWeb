using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdmin.Models
{
    public class DataResults<T>
    {
        public DataResults()
        {
            this.Success = false;
        }
        public string Msg { get; set; }
        public T Data { get; set; }
        public bool Success { get; set; }
    }
    public class Result
    {
        public Result()
        {
            this.Success = false;
        }
        public string Msg { get; set; }
        public bool Success { get; set; }
    }
}