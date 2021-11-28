using System;
using System.Collections.Generic;
using System.Text;

namespace Global.DTO
{
    public class ApiResult
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
