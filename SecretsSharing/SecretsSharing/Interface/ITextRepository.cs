using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SecretsSharing.DTO;

namespace SecretsSharing.Interface
{
    public interface ITextRepository
    {
        public Task<Guid> AddAsync(UserText model);
        public UserText GetById(Guid id);
        public Task Delete(Guid id);
        public List<UserText> GetAllForUser(Guid userId);
    }
}