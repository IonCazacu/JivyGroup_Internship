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
        private readonly Mock<IUserService> _userServiceMock = new();

        [Fact]
        public async Task LoginUserWithValidCredentialsReturnsUser()
        {
            Auth authModel = new()
            {
                Username = "testuser",
                Password = "testuser_password"
            };

            User user = new()
            {
                Username = "testuser",
                Email = "testuser@mail.com",
                Password = "testuser_password",
                ConfirmPassword = "testuser_password"
            };

            _userServiceMock
                .Setup(service => service.Login(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            var controller = new UserController(_userServiceMock.Object);
            var result = await controller.Login(authModel);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var objectResultValue = Assert.IsType<User>(okResult.Value);
            Assert.NotNull(objectResultValue);
            Assert.Equal(user, objectResultValue);
        }

        [Fact]
        public async Task LoginUserWithNullCredentialsReturnsBadRequest()
        {
            Auth authModel = new()
            {
                Username = null,
                Password = null
            };

            _userServiceMock
                .Setup(service => service.Login(It.IsAny<string>(), It.IsAny<string>()))
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
            _userServiceMock
                .Setup(service => service.Login(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(null as User);

            var controller = new UserController(_userServiceMock.Object);
            var result = await controller.Login(new Auth
            {
                Username = "WrongTestUser",
                Password = "WrongTestUsersPassword"
            });

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<NotFoundObjectResult>(userActionResult.Result);
            Assert.Equal(404, statusCode.StatusCode);
        }

        [Fact]
        public async Task LoginUserReturnsException()
        {
            _userServiceMock
                .Setup(service => service.Login(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Unknown exception"));

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Login(new Auth
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
        private readonly Mock<IUserService> _userServiceMock = new();

        [Fact]
        public async Task GetAllUsersReturnsOkObjectResult()
        {
            int cursor = 0;
            int limit = 1;

            User[] users = new[]
            {
                new User
                {
                    Id = 1,
                    Uuid = Guid.NewGuid(),
                    Username = "first_user",
                    Email = "first_user@mail.com",
                    Password = "first_users_password",
                    ConfirmPassword = "first_users_password"
                },
                new User
                {
                    Id = 2,
                    Uuid = Guid.NewGuid(),
                    Username = "second_user",
                    Email = "second_user@mail.com",
                    Password = "second_users_password",
                    ConfirmPassword = "second_users_password"
                },
                new User
                {
                    Id = 3,
                    Uuid = Guid.NewGuid(),
                    Username = "third_user",
                    Email = "third_user@mail.com",
                    Password = "third_users_password",
                    ConfirmPassword = "third_users_password"
                },
            };

            // `Callback` method is used to intercept the arguments passed to the GetUsers method
            _userServiceMock
                .Setup(service => service.GetUsers(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((users, 1))
                .Callback<int, int>((c, l) =>
                {
                    if (l < 50)
                    {
                        l = 50;
                    }
                });

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Get(cursor, limit);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);

            var paginationModel = Assert.IsType<Pagination>(okResult.Value);
            Assert.Equal(users, paginationModel.Users);
        }

        [Fact]
        public async Task GetAllUsersReturnsNotFoundObjectResult()
        {
            int cursor = 0;
            int limit = 1;

            _userServiceMock
                .Setup(service => service.GetUsers(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((Array.Empty<User>(), 1))
                .Callback<int, int>((c, l) =>
                {
                    if (l < 50)
                    {
                        l = 50;
                    }
                });

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Get(cursor, limit);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetAllUsersReturnsException()
        {
            int cursor = 0;
            int limit = 1;

            _userServiceMock
                .Setup(service => service.GetUsers(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Unknown exception"));

            var controller = new UserController(_userServiceMock.Object);
            var result = await controller.Get(cursor, limit);

            var enumUserActionResult = Assert.IsType<ActionResult<Pagination>>(result);
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

            User user = new()
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

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var objectResultValue = Assert.IsType<User>(okResult.Value);
            Assert.NotNull(objectResultValue);
            Assert.Equal(user, objectResultValue);
        }

        [Fact]
        public async Task GetUserByIdWithInvalidIdReturnsNotFound()
        {
            int userId = -1;

            _userServiceMock
                .Setup(service => service.GetUser(It.IsAny<int>()))
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
                .Setup(service => service.GetUser(It.IsAny<int>()))
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
        private readonly Mock<IUserService> _userServiceMock = new();

        [Fact]
        public async Task PostUserWithValidCredentialsReturnsUserCreated()
        {
            User user = new()
            {
                Id = 34,
                Uuid = Guid.NewGuid(),
                Username = "NonExistingUsernameInDb",
                Email = "NonExistingEmailInDb@mail.com",
                Password = "NonExistingUsernamesPassword123",
                ConfirmPassword = "NonExistingUsernamesPassword123"
            };

            _userServiceMock
                .Setup(service => service.AddUser(It.IsAny<User>()))
                .ReturnsAsync(user);

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Post(user);

            var okResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(okResult);
            Assert.Equal(201, okResult.StatusCode);

            var addedUserValue = Assert.IsType<User>(okResult.Value);
            Assert.NotNull(addedUserValue);
            Assert.Equal(user, addedUserValue);
        }

        [Fact]
        public async Task PostUserWithInvalidCredentialsReturnsBadRequest()
        {
            User user = new()
            {
                Id = 234,
                Uuid = Guid.NewGuid(),
                Username = null,
                Email = null,
                Password = null,
                ConfirmPassword = null
            };

            _userServiceMock
                .Setup(service => service.AddUser(It.IsAny<User>()))
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
            User userToAdd = new()
            {
                Username = "username",
                Email = "Test_User_1@mail.com",
                Password = "Username1!",
                ConfirmPassword = "Username1!"
            };

            _userServiceMock
                .Setup(service => service.AddUser(It.IsAny<User>()))
                .ThrowsAsync(new IntegrityException("Unknown exception"));

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Post(userToAdd);

            var userActionResult = Assert.IsType<ConflictObjectResult>(result.Result);
            Assert.NotNull(userActionResult);
            Assert.Equal(409, userActionResult.StatusCode);
        }

        [Fact]
        public async Task PostUserReturnsException()
        {
            User userToAdd = new()
            {
                Username = "username",
                Email = "Test_User_1@mail.com",
                Password = "Username1!",
                ConfirmPassword = "Username1!"
            };

            _userServiceMock
                .Setup(service => service.AddUser(It.IsAny<User>()))
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
        private readonly Mock<IUserService> _userServiceMock = new();

        [Fact]
        public async Task PutUserWithValidCredentialsReturnsUser()
        {
            int userId = 1;

            User user = new()
            {
                Id = 1,
                Username = "username",
                Email = "email@mail.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            _userServiceMock
                .Setup(service => service.UpdateUser(It.IsAny<int>(), It.IsAny<User>()))
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

            User user = new() { };

            _userServiceMock
                .Setup(service => service.UpdateUser(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(null as User);

            var controller = new UserController(_userServiceMock.Object);
            var result = await controller.Put(userId, user);

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<BadRequestObjectResult>(userActionResult.Result);
            Assert.Equal(400, statusCode.StatusCode);
        }

        [Fact]
        public async Task PutUserWithWrongCredentialsReturnsNotFoundObjectResult()
        {
            int userId = 1;

            User user = new()
            {
                Id = 1,
                Username = "username",
                Email = "email@mail.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            _userServiceMock
                .Setup(service => service.UpdateUser(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(null as User);

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Put(userId, user);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.NotNull(notFound);
            Assert.Equal(404, notFound.StatusCode);
        }

        [Fact]
        public async Task PutUserReturnsException()
        {
            int userId = 1;

            User user = new()
            {
                Id = 1,
                Username = "username",
                Email = "email@mail.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            _userServiceMock
                .Setup(service => service.UpdateUser(It.IsAny<int>(), It.IsAny<User>()))
                .ThrowsAsync(new Exception("Unknown exception"));

            var controller = new UserController(_userServiceMock.Object);

            var result = await controller.Put(userId, user);

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<ObjectResult>(userActionResult.Result);
            Assert.Equal(500, statusCode.StatusCode);
        }
    }

    public class DeleteUserMethod
    {
        private readonly Mock<IUserService> _userServiceMock = new();

        [Fact]
        public async Task DeleteUserByIdWithValidIdReturnsUser()
        {
            int userId = 123;

            User user = new()
            {
                Id = 123,
                Uuid = Guid.NewGuid(),
                Username = "testuser",
                Email = "testuser@mail.com",
                Password = "testuser_password",
                ConfirmPassword = "testuser_password"
            };

            _userServiceMock
                .Setup(service => service.DeleteUser(It.IsAny<int>()))
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
                .Setup(service => service.DeleteUser(It.IsAny<int>()))
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
                .Setup(service => service.DeleteUser(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Unknown exception"));

            var controller = new UserController(_userServiceMock.Object);
            var result = await controller.Delete(userId);

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<ObjectResult>(userActionResult.Result);
            Assert.Equal(500, statusCode.StatusCode);
        }
    }
}
