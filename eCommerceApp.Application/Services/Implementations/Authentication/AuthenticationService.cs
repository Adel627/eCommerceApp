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
            var mappedData = _mapper.Map<AppUser>(user);
            mappedData.UserName = user.Email;
            mappedData.PasswordHash = user.Password;

           var result = await _userManagement.CreateUser(mappedData);
            if (!result) 
                return new ServiceResponse(false, "Already user found with this email. ");
           var IsAdded = await _roleManagement.AddUserToRole(mappedData, "User");
            if (IsAdded) 
                return new ServiceResponse(true, "User Created");

            _logger.LogError(new Exception(
                   $"User With email : {mappedData.Email} failed to add to role : User"),
                   "User can not add to User role");
            return new ServiceResponse(false, "An error occure when add User to the Role");
        
        }

        public Task<LoginResponse> LoginUser(LoginUser user)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponse> ReviveToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
