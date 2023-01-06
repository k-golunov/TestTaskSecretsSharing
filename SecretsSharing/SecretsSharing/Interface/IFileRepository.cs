using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SecretsSharing.DTO;

namespace SecretsSharing.Interface
{
    public interface IFileRepository
    {
        public List<File> GetAll();
        public File GetById(Guid id);
        public Task<Guid> AddAsync(File file);
        public Task DeleteFile(File file);
    }
}