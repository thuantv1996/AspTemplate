using BusinessLogic.Services.Abstract;
using DataAccess.Abstract;
using DataAccess.Models.Identity;
using Global.DTO;
using Global.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserClaim> _userClaimRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork,
            IRepository<User> userRepository,
            IRepository<UserClaim> userClaimRepository,
            IRepository<UserRole> userRoleRepository,
            IRepository<Role> roleRepository,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userClaimRepository = userClaimRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
        }

        public AddUserResult AddUser(UserDTO userRegister)
        {
            string error = ValidateUser(userRegister);
            if(error != null)
            {
                return new AddUserResult
                {
                    IsSuccess = false,
                    Message = error
                };
            }

            string passwordSalt = Guid.NewGuid().ToString();
            User user = new User
            {
                AccessFailedCount = 0,
                CreatedBy = "system",
                Email = userRegister.Email,
                PasswordSalt = passwordSalt,
                PasswordHash = HashPassword(userRegister.Username, userRegister.Password, passwordSalt),
                Username = userRegister.Username,
                IsActive = true
            };
            _userRepository.Add(user);
            _unitOfWork.Commit();
            return new AddUserResult
            {
                IsSuccess = true,
                Message = "tạo tài khoản thành công"
            };
        }

        public LoginResult Login(UserDTO userLogin)
        {
            User user = _userRepository.Select(c=>c.Username == userLogin.Username && c.IsActive).FirstOrDefault();
            if(user == null)
            {
                return new LoginResult
                {
                    IsSuccess = false,
                    Message = "Đăng nhập thất bại!",
                    Token = null
                };
            }
            string hashPasswordlogin = HashPassword(userLogin.Username, userLogin.Password, user.PasswordSalt);
            if(user.PasswordHash != hashPasswordlogin)
            {
                return new LoginResult
                {
                    IsSuccess = false,
                    Message = "Đăng nhập thất bại!",
                    Token = null
                };
            }
            List<Claim> claims= GetUserClaims(user);
            string token = GenerateJwtToken(claims);
            return new LoginResult
            {
                IsSuccess = true,
                Message = "Đăng nhập thành công!",
                Token = token
            };
        }

        private string ValidateUser(UserDTO userRegister)
        {
            if (String.IsNullOrEmpty(userRegister.Username))
            {
                return "Tài khoản không được bỏ trống";
            }
            if (String.IsNullOrEmpty(userRegister.Password))
            {
                return "mật khẩu không được bỏ trống";
            }
            return null;
        }

        private string HashPassword(string username, string password, string passwordSalt)
        {
            string hashUsername = Utils.HashSHA256(username);
            string hashPassword = Utils.HashSHA256(password);
            string result = Utils.HashSHA256(hashUsername + Constant.CHAR_DOT + hashPassword + Constant.CHAR_DOT + passwordSalt);
            return result;
        }

        private List<Claim> GetUserClaims(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim("Username", user.Username));
            //claims.Add(new Claim("Email", user.Email));

            List<Role> roles = GetRolebyUser(user.Id);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var userClaims = _userClaimRepository.Select(c => c.UserId == user.Id).ToList();
            foreach(var userClaim in userClaims)
            {
                claims.Add(new Claim(userClaim.Name, userClaim.Value));
            }
            return claims;
        }

        private List<Role> GetRolebyUser(int userId)
        {
            var roles = from role in _roleRepository.SelectAll()
                        join userRole in _userRoleRepository.Select(c => c.UserId == userId)
                        on role.Id equals userRole.RoleId
                        select role;
            return roles.ToList();
        }

        private string GenerateJwtToken(List<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              null,
              DateTime.UtcNow.AddDays(Double.Parse(_configuration["Jwt:ExpireDays"])),
              credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
