using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SecretsSharing.DTO;
using SecretsSharing.Model;

namespace SecretsSharing.Interface
{
    public interface IFileManager
    {
        public Task<Guid> UploadFileAsync(UploadFileModel model, IFormFile file);
        public File GetFile(Guid id);
        public bool DeleteFile(Guid id);
        public void DeleteAutomatically(File file);
    }
}