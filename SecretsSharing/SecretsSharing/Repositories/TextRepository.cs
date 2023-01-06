using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretsSharing.DTO;
using SecretsSharing.Interface;
using SecretsSharing.Model;

namespace SecretsSharing.Repositories
{
    public class TextRepository : ITextRepository
    {
        private readonly DataContext _context;
        
        public TextRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Guid> AddAsync(UserText model)
        {
            var result = await _context.Set<UserText>().AddAsync(model); 
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }
        
        public UserText GetById(Guid id) => _context.Set<UserText>().FirstOrDefault(t => t.Id == id);
        
        public async Task Delete(Guid id)
        {
            var text = _context.Set<UserText>().FirstOrDefault(t => t.Id == id);
            _context.Text.Remove(text);
            await _context.SaveChangesAsync();
        }
        
        public List<UserText> GetAllForUser(Guid userId) => _context.Set<UserText>().Where(t => t.UserId == userId).ToList();
    }
}