using System;

namespace SecretsSharing.DTO
{
    public class File : BaseDto
    {
        public Guid UserId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
    }
}