using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SecretsSharing.DTO;

namespace SecretsSharing.Interface
{
    public interface IUserRepository
    {
        public Task<Guid> AddAsync(User user);
        public List<UserText> GetAllUserTexts(Guid userId);
        public List<File> GetAllUserFiles(Guid userId);
        public User GetById(Guid id);
        public List<User> GetAll();
        public Task<Guid> UpdateAsync(User entity);
    }
}