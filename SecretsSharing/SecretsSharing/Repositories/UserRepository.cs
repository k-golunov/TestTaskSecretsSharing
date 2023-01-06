using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretsSharing.DTO;
using SecretsSharing.Interface;

namespace SecretsSharing.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        
        public async Task<Guid> AddAsync(User user)
        {
            var result = await _context.Set<User>().AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }
        
        public User GetById(Guid id) => _context.Users.FirstOrDefault(u => u.Id == id);
        
        public List<User> GetAll() => _context.Set<User>().ToList();

        public List<File> GetAllUserFiles(Guid userId)
        {
            return _context.Set<File>().Where(f => f.UserId == userId).ToList();
        }
        
        public List<UserText> GetAllUserTexts(Guid userId)
        { 
            return _context.Set<UserText>().Where(f => f.UserId == userId).ToList();
        }
    }
}