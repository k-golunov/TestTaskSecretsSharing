using System;
using System.Threading.Tasks;
using SecretsSharing.DTO;

namespace SecretsSharing.Interface
{
    public interface ITextRepository
    {
        public Task<Guid> AddAsync(UserText model);
    }
}