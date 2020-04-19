using CeloTest.Repo;
using CeloTest.Service;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using CeloTest.Domain;
using System.Collections.Generic;

namespace Celo.Test
{
    public class UserServiceTest
    {
        private Mock<IUserRepo> _mockRepo;

        public UserServiceTest()
        {
            _mockRepo = new Mock<IUserRepo>();
        }

        [Fact]
        public void Should_Retrieve_Specific_User()
        {
            //Arrange
            var firstName = "Retrieve";
            var user = new User() { FirstName = firstName };
            IList<User> list = new List<User>();
            list.Add(user);
            IEnumerable<User> users = list;

            _mockRepo.Setup(s => s.Get(It.IsAny<Func<User, bool>>(), null, null))
                .Returns(Task.FromResult(users));
            IUserService service = new UserService(_mockRepo.Object);

            //Act
            var result = service.GetSingle(firstName, null, null, null, null, null, null).Result;

            //Assert
            Assert.Equal(firstName, user.FirstName);
            _mockRepo.Verify(r => r.Get(It.IsAny<Func<User, bool>>(), null, null), Times.Once);
        }

        [Fact]
        public void Should_Retrieve_Specific_User_With_Valid_Dob()
        {
            //Arrange
            var dob = new DateTime(2000, 10, 1);
            var user = new User() { DateOfBirth = dob };
            IList<User> list = new List<User>();
            list.Add(user);
            IEnumerable<User> users = list;
            _mockRepo.Setup(s => s.Get(It.IsAny<Func<User, bool>>(), null, null))
                .Returns(Task.FromResult(users));
            IUserService service = new UserService(_mockRepo.Object);

            //Act
            var result = service.GetSingle(null, null, null, null, "2000/10/01", null, null).Result;

            //Assert
            Assert.Equal(dob, result.DateOfBirth);
            _mockRepo.Verify(r => r.Get(It.IsAny<Func<User, bool>>(), null, null), Times.Once);
        }

        [Fact]
        public void Should_Retrieve_Specific_User_With_Invalid_Dob()
        {
            //Arrange
            var dob = new DateTime(2000, 10, 1);
            var user = new User() { DateOfBirth = dob };
            IList<User> list = new List<User>();
            list.Add(user);
            IEnumerable<User> users = list;
            _mockRepo.Setup(s => s.Get(It.IsAny<Func<User, bool>>(), null, null))
                .Returns(Task.FromResult(users));
            IUserService service = new UserService(_mockRepo.Object);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(() => service.GetSingle(null, null, null, null, "2000/13/13", null, null));
        }

        [Fact]
        public void Should_Update_User()
        {
            //Arrange
            var user = new User() { FirstName = "Update" };
            IList<User> list = new List<User>();
            list.Add(user);
            IEnumerable<User> users = list;
            _mockRepo.Setup(s => s.Update(It.Is<User>(c => c.FirstName == "Update")));
            IUserService service = new UserService(_mockRepo.Object);

            //Act
            service.Update(user);

            //Assert
            _mockRepo.Verify(r => r.Update(It.Is<User>(c => c.FirstName == "Update")), Times.Once);
        }

        [Fact]
        public void Should_Get_All_Users()
        {
            //Arrange
            IList<User> users = new List<User>();
            users.Add(new User());
            users.Add(new User());
            _mockRepo.Setup(s => s.GetAll())
                .Returns(Task.FromResult(users));
            IUserService service = new UserService(_mockRepo.Object);

            //Act
            var result = service.GetAll().Result;

            //Assert
            Assert.Equal(2, result.Count);
            _mockRepo.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public void Should_Delete_User()
        {
            //Arrange
            var user = new User() { FirstName = "Delete" };
            IList<User> list = new List<User>();
            list.Add(user);
            IEnumerable<User> users = list;
            _mockRepo.Setup(s => s.Delete(user));
            IUserService service = new UserService(_mockRepo.Object);

            //Act
            service.Delete(user);

            //Assert
            _mockRepo.Verify(r => r.Delete(user), Times.Once);
        }

        [Fact]
        public void Should_Filter_Users()
        {
            //Arrange
            var firstName = "something";
            IList<User> list =  new List<User>();
            list.Add(new User() { FirstName = firstName });
            list.Add(new User() { FirstName = firstName });
            IEnumerable<User> users = list;
            _mockRepo.Setup(s => s.Get(It.IsAny<Func<User, bool>>(), null, null))
                .Returns(Task.FromResult(users));
            IUserService service = new UserService(_mockRepo.Object);

            //Act
            var result = service.Filter(firstName, null, null, null, null, null, null).Result;

            //Assert
            Assert.Equal(2, result.Count);
            _mockRepo.Verify(r => r.Get(It.IsAny<Func<User, bool>>(), null, null), Times.Once);
        }
    }
}
