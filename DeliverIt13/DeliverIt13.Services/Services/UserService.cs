using System;
using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DeliverIt13.Services
{
	public class UserService : IUserService
	{
		private readonly DeliverItContext dbContext;
		public UserService(DeliverItContext dbContext)
		{
			this.dbContext = dbContext;
		}


        public UserAuthDTO GetByEmail(string email)
        {
            if (!IsValidEmail(email))
            {
                throw new Exception($"Input \"{email}\" must be a valid email address.");
            }
            var user = this.dbContext.Users.FirstOrDefault(u => u.Email == email) ?? throw new Exception($"No user fount with this email: {email}");

            var userDTO = new UserAuthDTO(user);

            return userDTO;
        }

        bool IsValidEmail(string email)
        {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }
    }
}
