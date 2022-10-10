

using StudentApi.Models;

namespace StudentApi.Core.Services.Auth
{
    public interface IUserService
    {
        Task<UserDTO> GetUser(User user);
    }

}