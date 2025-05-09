using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(User user, string password)
        {
            // Check if a user with the same username already exists
            if (_userRepository.GetAll().Any(u => u.Username == user.Username))
            {
                throw new ArgumentException("Username already exists");
            }

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            _userRepository.Add(user);
        }

        public User Login(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid login credentials");

            return user;
        }
    }
}
