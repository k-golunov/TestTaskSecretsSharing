using System;

namespace SecretsSharing.DTO
{
    public class UserText : BaseDto
    {
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public bool IsDelete { get; set; }
    }
}