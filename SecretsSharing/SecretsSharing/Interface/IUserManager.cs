using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SecretsSharing.DTO;
using SecretsSharing.Model;

namespace SecretsSharing.Interface
{
    public interface IUserManager
    {
        public AuthenticateResponse Authenticate(AuthModel model);
        public Task<AuthenticateResponse> Register(AuthModel userModel);
        public List<File> GetAllUserFiles(Guid userId);
        public List<UserText> GetAllUserTexts(Guid userId);
        public User GetById(Guid id);
    }
}