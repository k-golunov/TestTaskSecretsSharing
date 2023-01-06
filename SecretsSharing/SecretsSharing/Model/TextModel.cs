using System;

namespace SecretsSharing.Model
{
    /// <summary>
    /// Model for input user text
    /// </summary>
    public class TextModel
    {
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public bool IsDelete { get; set; }
    }
}