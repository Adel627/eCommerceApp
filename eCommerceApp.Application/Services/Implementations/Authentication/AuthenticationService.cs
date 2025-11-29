using eCommerceApp.Application.Consts;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Authentication;
using eCommerceApp.Application.Services.Interfaces.Authentication;
using eCommerceApp.Application.Services.Interfaces.Logging;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Services.Implementations.Authentication
{
    public class AuthenticationService (IUserManagement userManagement ,
        ITokenManagement tokenManagement , IRoleManagement roleManagement , IMapper mapper
         , IAppLogger<AuthenticationService> logger): IAuthenticationService
    {
        private readonly IUserManagement _userManagement = userManagement;
        private readonly ITokenManagement _tokenManagement = tokenManagement;
        private readonly IRoleManagement _roleManagement = roleManagement;
        private readonly IMapper _mapper = mapper;
        private readonly IAppLogger<AuthenticationService> _logger = logger;

        public async Task<ServiceResponse> CreateUser(CreateUser user)
        {
            if(await _userManagement.CheckeByUserName(user.UserName))
                return new ServiceResponse(false, "Already user found with this userName. ");

            if(await _userManagement.CheckeByPhoneNumber(user.PhoneNumber))
                return new ServiceResponse(false, "Already user found with this phoneNumber. ");


            var mappedData = _mapper.Map<AppUser>(user);
            mappedData.PasswordHash = user.Password;

           var result = await _userManagement.CreateUser(mappedData);
            if (!result) 
                return new ServiceResponse(false, "Already user found with this email. ");
         
            var IsAdded = await _roleManagement.AddUserToRole(mappedData, Roles.User);
            if (IsAdded) 
                return new ServiceResponse(true, "User Created");

            _logger.LogError(new Exception(
                   $"User With email : {mappedData.Email} failed to add to role : User"),
                   "User can not add to User role");
            return new ServiceResponse(false, "An error occure when add User to the Role");
        }

        public async Task<LoginResponse> LoginUser(LoginUser user)
        {

            var mappedData = _mapper.Map<AppUser>(user);
            mappedData.PasswordHash = user.Password;
            mappedData.Email = user.EmailorUserName;

            bool result = await _userManagement.LoginUser(mappedData);
            if(!result)
                return new LoginResponse(Message:"Invalid Email / UserName or Password");
           
            var appUser = await _userManagement.GetUserByEmailOrUserName(user.EmailorUserName);
            var claims = await _userManagement.GetUserClaims(appUser!.Email!);

            var jwtToken =  _tokenManagement.GenerateToken(claims);
            var refreshToken = _tokenManagement.GetRefreshToken();

              int  saveTokenResult = await _tokenManagement.AddRefreshToken(appUser.Id, refreshToken);
            return saveTokenResult > 0 ?
             new LoginResponse( Success: true, Token: jwtToken, RefreshToken: refreshToken)
             : new LoginResponse(Message: "Internal error occured while Authentication");
        }

        public async Task<LoginResponse> ReviveToken(string refreshToken)
        {
            bool valid = await _tokenManagement.ValidateRefreshToken(refreshToken);
            if (!valid)
               return new LoginResponse(Message: "Invalid token");

            var userId = await _tokenManagement.GetUserIdByRefreshToken(refreshToken);
            AppUser user = await _userManagement.GetUserById(userId);

            var claims = await _userManagement.GetUserClaims(user.Email!);
            var jwtToken = _tokenManagement.GenerateToken(claims);

            var newRefreshToken = _tokenManagement.GetRefreshToken();
           int update = await _tokenManagement.UpdateRefreshToken(userId,refreshToken , newRefreshToken);

           return update <= 0 ?
                 new LoginResponse(Message: "Invalid token")
                :new LoginResponse(Success: true, Token: jwtToken, RefreshToken: newRefreshToken);
                 

        }
    }
}
