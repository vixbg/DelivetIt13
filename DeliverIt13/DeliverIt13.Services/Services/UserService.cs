using System;
using System.Collections.Generic;
using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using System.Linq;
using DeliverIt13.Services.Models.ParcelDTOs;
using DeliverIt13.Services.Models.UserDTOs;
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

        public UserAuthDTO Get(int id)
        {
            var user = this.dbContext.Users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                throw new Exception("No user found with this ID.");
            }

            var newDTO = new UserAuthDTO(user);

            return newDTO;
        }

        public List<UserAuthDTO> GetAll()
        {
            var users = this.dbContext
                .Users
                .ToList();

            if (users.Count == 0)
            {
                throw new Exception("No users found.");
            }

            var userDTOs = new List<UserAuthDTO>();

            foreach (var user in users)
            {
                var newDTO = new UserAuthDTO(user);
                userDTOs.Add(newDTO);
            }

            return userDTOs;
        }
        public UserCreateDTO Create(UserCreateDTO user)
        {
            if (user == null)
            {
                throw new Exception("Input User is Empty or Null.");
            }

            var newUser = new User();
            newUser.Email = user.Email;
            newUser.Password = user.Password;
            newUser.Type = user.Type;

            this.dbContext.Users.Add(newUser);
            this.dbContext.SaveChanges();

            return user;

        }

        public int CreatePublic(UserCreatePublicDTO user)
        {
            if (user == null)
            {
                throw new Exception("Input User is Empty or Null.");
            }

            var newUser = new User();
            newUser.Email = user.Email;
            newUser.Password = user.Password;
            

            this.dbContext.Users.Add(newUser);
            this.dbContext.SaveChanges();
            var newUserId = newUser.UserId;

            return newUserId;

        }

        public bool Delete(int id)
        {
            var user = this.dbContext.Users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                throw new Exception("No user found with this ID.");
            }

            this.dbContext.Users.Remove(user);
            this.dbContext.SaveChanges();

            return true;

        }

        public UserUpdateDTO Update(UserUpdateDTO userDTO)
        {
            if (userDTO == null)
            {
                throw new Exception("Input User is Empty or Null.");
            }

            var user = this.dbContext.Users.FirstOrDefault(u => u.UserId == userDTO.UserId);

            if (user == null)
            {
                throw new Exception("No User found with this ID.");
            }

            user.Email = userDTO.Email;
            user.Password = userDTO.Password;
            user.Type = userDTO.Type;
            this.dbContext.SaveChanges();

            return userDTO;
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
            try 
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch 
            {
                return false;
            }
        }
    }
}
