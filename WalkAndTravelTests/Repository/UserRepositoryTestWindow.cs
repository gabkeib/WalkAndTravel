using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkAndTravelTests.Controllers;
using Xunit;

namespace WalkAndTravelTests.Repository
{
    public class UserRepositoryTestWindow
    {
        private SqliteUserRepositoryTest _sqliteUserRepositoryTest;
        public UserRepositoryTestWindow()
        {
            _sqliteUserRepositoryTest = new SqliteUserRepositoryTest();
        }

        [Fact]
        public void UserRepository_GetUsers_ReturnsCorrectAmountOfUsers()
        {
            _sqliteUserRepositoryTest.UserRepository_GetUsers_ReturnsCorrectAmountOfUsers();
        }

        [Theory]
        [InlineData ("badmail@two.com", "Jurgita")]
        [InlineData ("ech@outbox.com", "Jin")]
        public void UserRepository_GetByEmail_ReturnsCorrectUser(string email, string name)
        {
            _sqliteUserRepositoryTest.UserRepository_GetByEmail_ReturnsCorrectUser(email, name);
        }

        [Theory]
        [InlineData(2, "Jurgita")]
        [InlineData(3, "Jin")]
        public void UserRepository_GetById_ReturnsCorrectUser(int id, string name)
        {
            _sqliteUserRepositoryTest.UserRepository_GetById_ReturnsCorrectUser(id, name);
        }

        [Theory]
        [InlineData(1, "Kazimeras")]
        [InlineData(3, "Jin")]
        public void UserRepository_DeleteUser_DeletesCorrectUser(int id, string name)
        {
            _sqliteUserRepositoryTest.UserRepository_DeleteUser_DeletesCorrectUser(id, name);
        }

        [Fact]
        public void UserRepository_CreateNewUser_AddsCorrectUser()
        {
            _sqliteUserRepositoryTest.UserRepository_CreateNewUser_AddsCorrectUser();
        }
    }
}

