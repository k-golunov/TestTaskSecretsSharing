using System;

namespace SecretsSharing.Model
{
    public class UploadFileModel
    {
        /// <summary>
        /// the unique identifier of the user who uploads the file
        /// </summary>
        public Guid UserId { get; set; }
        
        /// <summary>
        /// a value that determines whether to delete a file after accessing it
        /// </summary>
        public bool IsDelete { get; set; }
    }
}