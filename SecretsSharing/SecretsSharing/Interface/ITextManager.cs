using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SecretsSharing.DTO;
using SecretsSharing.Model;

namespace SecretsSharing.Interface
{
    public interface ITextManager
    {
        public Task<Guid> UploadText(TextModel model);
        public UserText GetText(Guid id);
        public void DeleteText(Guid id);
        public List<UserText> GetAllForUser(Guid userId);
    }
}