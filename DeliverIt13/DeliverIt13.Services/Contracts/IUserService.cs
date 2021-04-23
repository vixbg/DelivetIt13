
using System.Collections.Generic;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Models;
using DeliverIt13.Services.Models.UserDTOs;

namespace DeliverIt13.Services.Contracts
{
	public interface IUserService
    {
        UserAuthDTO Get(int id);
        List<UserAuthDTO> GetAll();
        UserCreateDTO Create(UserCreateDTO user);
        int CreatePublic(UserCreatePublicDTO user);
        bool Delete(int id);
        UserUpdateDTO Update(UserUpdateDTO userDTO);
        UserAuthDTO GetByEmail(string email);
    }
}
