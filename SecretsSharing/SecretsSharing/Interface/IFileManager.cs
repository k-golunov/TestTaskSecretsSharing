using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SecretsSharing.Model;

namespace SecretsSharing.Interface
{
    public interface IFileManager
    {
        public Task<Guid> UploadFileAsync(UploadFileModel model, IFormFile file);
    }
}