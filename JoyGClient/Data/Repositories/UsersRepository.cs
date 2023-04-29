using AutoMapper;
using JoyGClient.Entities;
using JoyGClient.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JoyGClient.Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UsersRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                .Include(p => p.UserRoles)
                .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            return await _context.Users
                .Include(p => p.UserRoles)
                .SingleOrDefaultAsync(x => x.Id == Convert.ToInt32(id));

        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(p => p.UserRoles)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.UserName == username);
        }
    }
}
