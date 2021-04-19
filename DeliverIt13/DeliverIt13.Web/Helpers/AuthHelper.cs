using System;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;

namespace DeliverIt13.Web.Helpers
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IUserService userService;

        public AuthHelper(IUserService userService)
        {
            this.userService = userService;
        }

        public UserAuthDTO TryGetUser(string credentialsHeader)
        {
            try
            {
                return this.userService.GetByEmail(credentialsHeader);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Invalid Username/Email.");
            }
        }
    }
}