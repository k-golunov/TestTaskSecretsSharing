namespace SecretsSharing.Model
{
    /// <summary>
    /// Model for input values in api for register and login user
    /// </summary>
    public class AuthModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}