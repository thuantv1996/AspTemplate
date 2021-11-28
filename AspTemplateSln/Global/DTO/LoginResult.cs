using System;
using System.Collections.Generic;
using System.Text;

namespace Global.DTO
{
    public class LoginResult
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
    }
}
