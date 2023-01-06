using System;
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
    }
}