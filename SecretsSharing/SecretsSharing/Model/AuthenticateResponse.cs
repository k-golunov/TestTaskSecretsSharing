using System;
using SecretsSharing.Attribute;
using SecretsSharing.DTO;

namespace SecretsSharing.Model
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        [Email]
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Email = user.Email;
            AccessToken = token;
        }
    }
}