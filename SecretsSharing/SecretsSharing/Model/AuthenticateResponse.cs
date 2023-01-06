using System;
using SecretsSharing.DTO;

namespace SecretsSharing.Model
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
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