
using StudentApi.Data;
using StudentApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace StudentApi.Core.Services.Auth
{


    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserDTO> GetUser(User user)
        {
            UserDTO _user = await _context.Users
            .Where(x => x.UserName == user.UserName)
             .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
             .FirstOrDefaultAsync();
            _user.Role = "Admin";
            return _user;

        }
    }

}