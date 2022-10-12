
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
        public async Task<UserDTO> GetUserByName(string userName)
        {
            UserDTO _user = await _context.Users
            .Where(x => x.UserName == userName)
             .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
             .FirstOrDefaultAsync();
            if (_user is not null)
                _user.Role = "Admin";
            return _user;
        }
        public async Task<UserDTO> GetUserById(long id)
        {
           UserDTO _user = await _context.Users
            .Where(x => x.ID == id)
             .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
             .FirstOrDefaultAsync();
            if (_user is not null)
                _user.Role = "Admin";
            return _user;
        }
        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> Insert(User user)
        {
            try
            {
                _context.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }
    }

}