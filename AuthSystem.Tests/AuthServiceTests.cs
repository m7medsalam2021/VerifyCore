using AuthSystem.Core.Entities;
using AuthSystem.Core.Interfaces;
using AuthSystem.Infrastructure.Services;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthSystem.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _authService = new AuthService(_userRepoMock.Object);
        }

        [Fact]
        public void Register_WithValidData_ShouldSucceed()
        {
        }

        [Fact]
        public void Register_WithExistingUsername_ShouldThrowException()
        {
            // Arrange
            var existingUser = new User { Username = "existing", Email = "exist@example.com" };
            _userRepoMock.Setup(r => r.GetAll()).Returns(new List<User> { existingUser }.AsQueryable());

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                _authService.Register(existingUser, "anypassword"));
            Assert.Contains("اسم المستخدم موجود مسبقاً", ex.Message);
        }

        [Fact]
        public void Login_WithCorrectCredentials_ShouldReturnUser()
        {
            // Arrange
            var correctPassword = "rightpass";
            var user = new User
            {
                Username = "testuser",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(correctPassword)
            };
            _userRepoMock.Setup(r => r.GetByUsername("testuser")).Returns(user);

            // Act
            var result = _authService.Login("testuser", correctPassword);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("testuser", result.Username);
        }

        [Fact]
        public void Login_WithWrongPassword_ShouldThrowException()
        {
            // Arrange
            var user = new User
            {
                Username = "testuser",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("rightpass")
            };
            _userRepoMock.Setup(r => r.GetByUsername("testuser")).Returns(user);

            // Act & Assert
            var ex = Assert.Throws<UnauthorizedAccessException>(() =>
                _authService.Login("testuser", "wrongpass"));
            Assert.Contains("Invalid Data", ex.Message);
        }
    }
}