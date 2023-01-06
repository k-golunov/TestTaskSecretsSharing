using System.Collections.Generic;

namespace SecretsSharing.DTO
{
    public class User : BaseDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public List<File> files { get; set; }
        public List<UserText> texts { get; set; }
    }
}