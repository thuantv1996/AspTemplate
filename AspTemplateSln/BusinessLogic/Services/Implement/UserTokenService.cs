using BusinessLogic.Services.Abstract;
using DataAccess.Abstract;
using DataAccess.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services.Implement
{
    public class UserTokenService : IUserTokenService
    {

        private readonly IUnitOfWork _unitOfWork;
        private IRepository<UserToken> _userTokenRepository;

        public UserTokenService(IUnitOfWork unitOfWork,
            IRepository<UserToken> userTokenRepository)
        {
            _unitOfWork = unitOfWork;
            _userTokenRepository = userTokenRepository;
        }
        public void AddUserToken(int userId, string token)
        {
            UserToken userToken = new UserToken
            {
                Name = "Test Token",
                UserId = userId,
                Value = token
            };
            _userTokenRepository.Add(userToken);
            _unitOfWork.Commit();
        }
    }
}
