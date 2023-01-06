using System;

namespace SecretsSharing.Model
{
    public class UploadFileModel
    {
        public Guid UserId { get; set; }
        public bool IsDelete { get; set; }
    }
}