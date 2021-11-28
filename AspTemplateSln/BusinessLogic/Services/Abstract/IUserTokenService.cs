using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services.Abstract
{
    public interface IUserTokenService
    {
        void AddUserToken(int userId, string token);
    }
}
