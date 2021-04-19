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
            var user = this.dbContext.Users.FirstOrDefault(u => u.Email == email) ?? throw new ArgumentNullException();

            var userDTO = new UserAuthDTO(user);

            return userDTO;
        }
    }
}
