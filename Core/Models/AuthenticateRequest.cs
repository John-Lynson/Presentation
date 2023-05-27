namespace Core.Models
{
    public class AuthenticateRequest
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public AuthenticateRequest(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            }

            Email = email;
            Password = password;
        }
    }
}
