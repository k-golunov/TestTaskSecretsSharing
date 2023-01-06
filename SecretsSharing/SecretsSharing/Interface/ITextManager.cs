using System;
using System.Threading.Tasks;
using SecretsSharing.Model;

namespace SecretsSharing.Interface
{
    public interface ITextManager
    {
        public Task<Guid> UploadText(TextModel model);
    }
}