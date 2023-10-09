using Microsoft.AspNetCore.Mvc;
using Moq;
using server.Services.UserServices.Controllers;
using server.Services.UserServices.Model;
using server.Services.UserServices.Ports;

namespace server.Tests;

public class UserControllerTests
{
    public class LoginMethod
    {
        private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();

        [Fact]
        public async Task LoginUserWithValidCredentialsReturnsUser()
        {
            AuthModel authModel = new AuthModel()
            {
                Username = "testuser",
                Password = "testuser_password"
            };

            User user = new User()
            {
                Id = 1,
                Uuid = Guid.NewGuid(),
                Username = "testuser",
                Email = "testuser@mail.com",
                Password = "testuser_password",
                ConfirmPassword = "testuser_password"
            };

            _userServiceMock
                .Setup(service => service.Login(
                    authModel.Username,
                    authModel.Password
                    )
                )
                .ReturnsAsync(user);

            var controller = new UserController(_userServiceMock.Object);
            var result = await controller.Login(authModel);

            var okResult = Assert.IsType<ActionResult<User>>(result);

            var objectResult = okResult.Result as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);

            var objectResultValue = objectResult.Value as User;
            Assert.NotNull(objectResultValue);
            Assert.Equal(user, objectResultValue);
        }

        [Fact]
        public async Task LoginUserWithNullCredentialsReturnsBadRequest()
        {
            AuthModel authModel = new AuthModel()
            {
                Username = null,
                Password = null
            };

            _userServiceMock
                .Setup(service => service.Login(
                    authModel.Username!,
                    authModel.Password!
                    )
                )
                .ReturnsAsync(null as User);

            var controller = new UserController(_userServiceMock.Object);
            var result = await controller.Login(authModel);

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<BadRequestObjectResult>(userActionResult.Result);
            Assert.Equal(400, statusCode.StatusCode);
        }

        [Fact]
        public async Task LoginUserWithWrongCredentialsReturnsNotFound()
        {
            AuthModel authModel = new AuthModel()
            {
                Username = "WrongTestUser",
                Password = "WrongTestUsersPassword"
            };

            _userServiceMock
                .Setup(service => service.Login(
                    authModel.Username,
                    authModel.Password
                    )
                )
                .ReturnsAsync(null as User);

            var controller = new UserController(_userServiceMock.Object);
            var result = await controller.Login(authModel);

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<NotFoundObjectResult>(userActionResult.Result);
            Assert.Equal(404, statusCode.StatusCode);
        }

        [Fact]
        public async Task LoginUserReturnsException()
        {
            _userServiceMock
                .Setup(service => service.Login(
                    It.IsAny<string>(),
                    It.IsAny<string>()
                    )
                )
                .ThrowsAsync(new Exception("Unknown exception"));

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Login(new AuthModel
            {
                Username = "test_username",
                Password = "test_password"
            });

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<ObjectResult>(userActionResult.Result);
            Assert.Equal(500, statusCode.StatusCode);
        }
    }

    public class GetUsersMethod
    {
        private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();

        [Fact]
        public async Task GetAllUsersReturnsException()
        {
            _userServiceMock
                .Setup(service => service.GetUsers())
                .ThrowsAsync(new Exception("Unknown exception"));

            var controller = new UserController(_userServiceMock.Object);
            var result = await controller.Get();

            var enumUserActionResult = Assert.IsType<ActionResult<IEnumerable<User>>>(result);
            var statusCode = Assert.IsType<ObjectResult>(enumUserActionResult.Result);
            Assert.Equal(500, statusCode.StatusCode);
        }
    }

    public class GetUserMethod
    {
        private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();

        [Fact]
        public async Task GetUserByIdWithValidIdReturnsUser()
        {
            int userId = 123;

            User user = new User()
            {
                Id = 123,
                Uuid = Guid.NewGuid(),
                Username = "testuser",
                Email = "testuser@mail.com",
                Password = "testuser_password",
                ConfirmPassword = "testuser_password"
            };

            _userServiceMock
                .Setup(service => service.GetUser(It.IsAny<int>()))
                .ReturnsAsync(user);

            var controller = new UserController(_userServiceMock.Object);
            var result = await controller.Get(userId);

            var okResult = Assert.IsType<ActionResult<User>>(result);

            var objectResult = okResult.Result as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);

            var objectResultValue = objectResult.Value as User;
            Assert.NotNull(objectResultValue);
            Assert.Equal(user, objectResultValue);
        }

        [Fact]
        public async Task GetUserByIdWithInvalidIdReturnsNotFound()
        {
            int userId = -1;

            _userServiceMock
                .Setup(service => service.GetUser(userId))
                .ReturnsAsync(null as User);

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Get(userId);

            var okResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<NotFoundObjectResult>(okResult.Result);
            Assert.Equal(404, statusCode.StatusCode);
        }

        [Fact]
        public async Task GetUserByIdReturnsException()
        {
            int userId = 1233;

            _userServiceMock
                .Setup(service => service.GetUser(userId))
                .ThrowsAsync(new Exception("Unknown exception"));

            var controller = new UserController(_userServiceMock.Object);
            var result = await controller.Get(userId);

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<ObjectResult>(userActionResult.Result);
            Assert.Equal(500, statusCode.StatusCode);
        }
    }

    public class PostUserMethod
    {
        private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();

        [Fact]
        public async Task PostUserWithValidCredentialsReturnsUserCreated()
        {
            User user = new User()
            {
                Id = 34,
                Uuid = Guid.NewGuid(),
                Username = "NonExistingUsernameInDb",
                Email = "NonExistingEmailInDb@mail.com",
                Password = "NonExistingUsernamesPassword123",
                ConfirmPassword = "NonExistingUsernamesPassword123"
            };

            _userServiceMock
                .Setup(service => service.AddUser(user))
                .ReturnsAsync(user);

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Post(user);

            var okResult = Assert.IsType<ActionResult<User>>(result);
            Assert.NotNull(okResult);

            var createdUser = okResult.Result as CreatedAtActionResult;
            Assert.NotNull(createdUser);
            Assert.IsType<CreatedAtActionResult>(createdUser);
            Assert.Equal(201, createdUser.StatusCode);

            var userValue = createdUser.Value;
            Assert.NotNull(userValue);

            var addedUserValue = userValue as User;
            Assert.NotNull(addedUserValue);
            Assert.Equal(user, addedUserValue);
        }

        [Fact]
        public async Task PostUserWithInvalidCredentialsReturnsBadRequest()
        {
            User user = new User()
            {
                Id = 234,
                Uuid = Guid.NewGuid(),
                Username = null,
                Email = null,
                Password = null,
                ConfirmPassword = null
            };

            _userServiceMock
                .Setup(service => service.AddUser(user))
                .ReturnsAsync(null as User);

            var controller = new UserController(_userServiceMock.Object);

            controller.ModelState.AddModelError("error", "Unknown error");

            var result = await controller.Post(user);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.NotNull(badRequestResult);
            Assert.IsType<BadRequestObjectResult>(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task PostUserWithExistingEmailReturnsConflictObjectResult()
        {
            User userToAdd = new User()
            {
                Username = "username",
                Email = "Test_User_1@mail.com",
                Password = "Username1!",
                ConfirmPassword = "Username1!"
            };

            _userServiceMock
                .Setup(service => service.AddUser(userToAdd))
                .ThrowsAsync(new IntegrityException("Unknown exception"));

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Post(userToAdd);
            Assert.NotNull(result);

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            Assert.NotNull(userActionResult);

            var conflictObjectResult = Assert.IsType<ConflictObjectResult>(userActionResult.Result);
            Assert.Equal(409, conflictObjectResult.StatusCode);
        }

        [Fact]
        public async Task PostUserReturnsException()
        {
            User userToAdd = new User()
            {
                Username = "username",
                Email = "Test_User_1@mail.com",
                Password = "Username1!",
                ConfirmPassword = "Username1!"
            };

            _userServiceMock
                .Setup(service => service.AddUser(userToAdd))
                .ThrowsAsync(new Exception("Unknown exception"));

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Post(userToAdd);

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<ObjectResult>(userActionResult.Result);
            Assert.Equal(500, statusCode.StatusCode);
        }
    }

    public class PutUserMethod
    {
        private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();

        [Fact]
        public async Task PutUserWithValidCredentialsReturnsUser()
        {
            int userId = 1;

            User user = new User()
            {
                Id = 1,
                Username = "username",
                Email = "email@mail.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            _userServiceMock
                .Setup(service => service.UpdateUser(userId, user))
                .ReturnsAsync(user);

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Put(userId, user);
            var userActionValue = Assert.IsType<ActionResult<User>>(result);
            Assert.Equal(user, userActionValue.Value);
        }

        [Fact]
        public async Task PutUserWithInvalidCredentialsReturnsBadRequestResult()
        {
            int userId = 2;

            User user = new User() { };

            _userServiceMock
                .Setup(service => service.UpdateUser(userId, user))
                .ReturnsAsync(null as User);

            var controller = new UserController(_userServiceMock.Object);
            var result = await controller.Put(userId, user);

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<BadRequestResult>(userActionResult.Result);
            Assert.Equal(400, statusCode.StatusCode);
        }

        [Fact]
        public async Task PutUserWithWrongCredentialsReturnsNotFoundObjectResult()
        {
            int userId = 1;

            User user = new User()
            {
                Id = 1,
                Username = "username",
                Email = "email@mail.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            _userServiceMock
                .Setup(service => service.UpdateUser(userId, user))
                .ReturnsAsync(null as User);

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Put(userId, user);
            Assert.NotNull(result);
            Assert.IsType<ActionResult<User>>(result);
            Assert.IsType<NotFoundObjectResult>(result.Result);

            var statusCode = result.Result as NotFoundObjectResult;
            Assert.NotNull(statusCode);
            Assert.Equal(404, statusCode.StatusCode);
        }

        [Fact]
        public async Task PutUserReturnsException()
        {
            int userId = 1;

            User user = new User()
            {
                Id = 1,
                Username = "username",
                Email = "email@mail.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            _userServiceMock
                .Setup(service => service.UpdateUser(userId, user))
                .ThrowsAsync(new Exception("Unknown exception"));

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Put(userId, user);

            Assert.NotNull(result);
            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<ObjectResult>(userActionResult.Result);
            Assert.Equal(500, statusCode.StatusCode);
        }
    }

    public class DeleteUserMethod
    {
        private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();

        [Fact]
        public async Task DeleteUserByIdWithValidIdReturnsUser()
        {
            int userId = 123;

            User user = new User()
            {
                Id = 123,
                Uuid = Guid.NewGuid(),
                Username = "testuser",
                Email = "testuser@mail.com",
                Password = "testuser_password",
                ConfirmPassword = "testuser_password"
            };

            _userServiceMock
                .Setup(service => service.DeleteUser(userId))
                .ReturnsAsync(user);

            var controller = new UserController(_userServiceMock.Object);
            
            var result = await controller.Delete(userId);
            var userActionValue = Assert.IsType<ActionResult<User>>(result);
            Assert.Equal(user, userActionValue.Value);
        }

        [Fact]
        public async Task DeleteUserByIdWithInvalidIdReturnsNotFound()
        {
            int userId = -1;

            _userServiceMock
                .Setup(service => service.DeleteUser(userId))
                .ReturnsAsync(null as User);

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Delete(userId);

            var okResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<NotFoundObjectResult>(okResult.Result);
            Assert.Equal(404, statusCode.StatusCode);
        }

        [Fact]
        public async Task GetUserByIdReturnsException()
        {
            int userId = 1233;

            _userServiceMock
                .Setup(service => service.DeleteUser(userId))
                .ThrowsAsync(new Exception("Unknown exception"));

            var controller = new UserController(_userServiceMock.Object);
            var result = await controller.Delete(userId);

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<ObjectResult>(userActionResult.Result);
            Assert.Equal(500, statusCode.StatusCode);
        }
    }
}
