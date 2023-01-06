using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretsSharing.DTO;
using SecretsSharing.Interface;

namespace SecretsSharing.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly DataContext _context;
        
        public FileRepository(DataContext context)
        {
            _context = context;
        }

        public List<File> GetAllForUser(Guid userId) => _context.Set<File>().Where(f => f.UserId == userId).ToList();  
        
        public File GetById(Guid id) => _context.Set<File>().FirstOrDefault(f => f.Id == id);
        
        public async Task<Guid> AddAsync(File file)
        {
            if (_context.Users.Any(x => x.Id == file.UserId))
            {
                var result = await _context.Set<File>().AddAsync(file); 
                await _context.SaveChangesAsync();
                return result.Entity.Id;
            }

            throw new Exception("User not found");
        }
        
        public async Task DeleteFile(File file)
        {
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
        }
    }
}