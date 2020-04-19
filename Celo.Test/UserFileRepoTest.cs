using CeloTest.Domain;
using CeloTest.Repo;
using CeloTest.Repo.Context;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CeloTest.Test
{
    public class UserFileRepoTest
    {
        private Mock<IFileContext> _mockContext;
        public UserFileRepoTest()
        {
            _mockContext = new Mock<IFileContext>();
        }

        [Fact]
        public void Should_Delete_User()
        {
            //Arrange
            var id = Guid.NewGuid();
            var users = new List<User>();
            var user = new User { Id = id, FirstName = "delete" };
            users.Add(user);
            _mockContext.Setup(c => c.Read()).Returns(users);
            UserFileRepo repo = new UserFileRepo(_mockContext.Object);

            //Act
            repo.Delete(user).GetAwaiter().GetResult();
            var result = repo.GetAll().GetAwaiter().GetResult();

            //Assert
            Assert.Equal(0, result.Count);
            _mockContext.Verify(c => c.Write(It.IsAny<IList<User>>()), Times.Once);
            repo.Dispose();
        }

        [Fact]
        public void Should_Upodate_User()
        {
            //Arrange
            var id = Guid.NewGuid();
            var users = new List<User>();
            var user = new User { Id = id, FirstName = "original" };
            var updatedUser = user;
            users.Add(user);
            _mockContext.Setup(c => c.Read()).Returns(users);
            UserFileRepo repo = new UserFileRepo(_mockContext.Object);
            updatedUser.FirstName = "Updated";

            //Act
            repo.Update(updatedUser).GetAwaiter().GetResult();
            var result = repo.GetAll().GetAwaiter().GetResult();

            //Assert
            Assert.Equal(1, result.Count);
            Assert.Equal("Updated", result[0].FirstName);
            _mockContext.Verify(c => c.Write(It.IsAny<IList<User>>()), Times.Once);
            repo.Dispose();
        }
    }
}
