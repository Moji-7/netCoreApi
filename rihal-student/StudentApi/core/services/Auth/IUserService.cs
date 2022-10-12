

using StudentApi.Models;

namespace StudentApi.Core.Services.Auth
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByName(string userName);
        Task<UserDTO> GetUserById(long id);
        Task<List<User>> GetAll();
        Task<User> Insert(User user);
    }

}