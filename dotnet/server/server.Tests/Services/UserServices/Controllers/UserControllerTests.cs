using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using server.Services.UserServices.Contracts;
using server.Services.UserServices.Controllers;
using server.Services.UserServices.Entities;
using server.Services.UserServices.Entities.Authorization;

namespace server.Tests;

public class UserControllerTests
{
    public class BasicAuthorizationMethod
    {
        // private readonly Mock<ILogger<UserController>> _loggerMock = new();
        private readonly Mock<IUserFacade> _userFacadeMock = new();

        [Fact]
        public async Task GetUser_WithValidCredentials_ReturnsOk()
        {
            BasicAuthorization userToAuthenticate = new()
            {
                Username = "testuser",
                Password = "testuser_password"
            };

            User user = new()
            {
                Id = 1,
                Uuid = Guid.NewGuid().ToString(),
                Username = "testuser",
                Email = "testuser@mail.com",
                Password = "testuser_password",
                ConfirmPassword = "testuser_password"
            };

            _userFacadeMock
                .Setup(service => service.AuthenticateUser(It.IsAny<BasicAuthorization>()))
                .ReturnsAsync(user);

            UserController controller = new(_userFacadeMock.Object);
            var result = await controller.AuthenticateUser(userToAuthenticate);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var resultValue = Assert.IsType<User>(okResult.Value);
            Assert.NotNull(resultValue);
            Assert.Equal(user, resultValue);
        }

        [Fact]
        public async Task GetUser_WithNullCredentials_ReturnsNotFound()
        {
            _userFacadeMock
                .Setup(service => service.AuthenticateUser(It.IsAny<BasicAuthorization>()))
                .ReturnsAsync(null as User);

            UserController controller = new(_userFacadeMock.Object);
            var result = await controller.AuthenticateUser(new BasicAuthorization
            {
                Username = null,
                Password = null
            });

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<NotFoundResult>(userActionResult.Result);
            Assert.Equal(404, statusCode.StatusCode);
        }

        [Fact]
        public async Task GetUser_WithWrongCredentials_ReturnsNotFound()
        {
            _userFacadeMock
                .Setup(service => service.AuthenticateUser(It.IsAny<BasicAuthorization>()))
                .ReturnsAsync(null as User);

            UserController controller = new(_userFacadeMock.Object);
            var result = await controller.AuthenticateUser(new BasicAuthorization
            {
                Username = "WrongTestUser",
                Password = "WrongTestUsersPassword"
            });

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<NotFoundResult>(userActionResult.Result);
            Assert.Equal(404, statusCode.StatusCode);
        }

        [Fact]
        public async Task GetUser_ReturnsException()
       {
           _userFacadeMock
               .Setup(service => service.AuthenticateUser(It.IsAny<BasicAuthorization>()))
               .ThrowsAsync(new Exception("Unknown exception"));

           UserController controller = new(_userFacadeMock.Object);

           var result = await controller.AuthenticateUser(new BasicAuthorization
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
        // private readonly Mock<ILogger<UserController>> _loggerMock = new();
        private readonly Mock<IUserFacade> _userFacadeMock = new();

        [Fact]
        public async Task GetUsers_ReturnsOk()
        {
            int cursor = 0;
            int limit = 1;
            
            User[] users = new[]
           {
               new User
               {
                   Id = 1,
                   Uuid = Guid.NewGuid().ToString(),
                   Username = "first_user",
                   Email = "first_user@mail.com",
                   Password = "first_users_password",
                   ConfirmPassword = "first_users_password"
               },
               new User
               {
                   Id = 2,
                   Uuid = Guid.NewGuid().ToString(),
                   Username = "second_user",
                   Email = "second_user@mail.com",
                   Password = "second_users_password",
                   ConfirmPassword = "second_users_password"
               },
               new User
               {
                   Id = 3,
                   Uuid = Guid.NewGuid().ToString(),
                   Username = "third_user",
                   Email = "third_user@mail.com",
                   Password = "third_users_password",
                   ConfirmPassword = "third_users_password"
               },
           };

            int nextCursor = 1;
            int maxId = users.Length - 1;
            bool hasNextCursor = nextCursor != maxId;

            // `Callback` method is used to intercept the arguments passed to the GetUsers method
            _userFacadeMock
                .Setup(service =>
                (
                    service.GetUsers(It.IsAny<int>(), It.IsAny<int>()))
                )
                .ReturnsAsync(new Pagination
                {
                    Users = users,
                    NextCursor = nextCursor
                })
                .Callback<int, int>((c, l) =>
                {
                    if (l < 50)
                    {
                        l = 50;
                    }
                });

           UserController controller = new(_userFacadeMock.Object);

           var result = await controller.GetUsers(cursor, limit);

           var okResult = Assert.IsType<OkObjectResult>(result.Result);
           Assert.Equal(200, okResult.StatusCode);

           var paginationModel = Assert.IsType<Pagination>(okResult.Value);
           Assert.Equal(users, paginationModel.Users);
       }

        [Fact]
        public async Task GetUsers_ReturnsNotFound()
        {
            int cursor = 0;
            int limit = 1;

            _userFacadeMock
                .Setup(service =>
                (
                    service.GetUsers(It.IsAny<int>(), It.IsAny<int>()))
                )
                .ReturnsAsync(new Pagination()
                {
                    Users = Array.Empty<User>(),
                    NextCursor = -1
                })
                .Callback<int, int>((c, l) =>
                {
                    if (l < 50)
                    {
                        l = 50;
                    }
                });

            UserController controller = new(_userFacadeMock.Object);

            var result = await controller.GetUsers(cursor, limit);

            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetUsers_ReturnsException()
        {
            int cursor = 0;
            int limit = 1;

            _userFacadeMock
                .Setup(service => service.GetUsers(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Unknown exception"));

            UserController controller = new(_userFacadeMock.Object);
            var result = await controller.GetUsers(cursor, limit);

            var enumUserActionResult = Assert.IsType<ActionResult<Pagination>>(result);
            var statusCode = Assert.IsType<ObjectResult>(enumUserActionResult.Result);
            Assert.Equal(500, statusCode.StatusCode);
        }
    }

    public class GetUserMethod
    {
        // private readonly Mock<ILogger<UserController>> _loggerMock = new();
        private readonly Mock<IUserFacade> _userFacadeMock = new();

        [Fact]
        public async Task GetUserById_WithValidId_ReturnsOk()
        {
            Guid userUuid = new("31b76663-790a-41ed-912f-d1a120d318b8");

            User user = new()
            {
                Id = 951,
                Uuid = "31b76663-790a-41ed-912f-d1a120d318b8",
                Username = "username951",
                Email = "username951@mail.com",
                Password = "username951Password!",
                ConfirmPassword = "username951Password!"
            };

            _userFacadeMock
                .Setup(service => service.GetUser(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            UserController controller = new(_userFacadeMock.Object);
            var result = await controller.GetUser(userUuid);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var objectResultValue = Assert.IsType<User>(okResult.Value);
            Assert.NotNull(objectResultValue);
            Assert.Equal(user, objectResultValue);
        }

        [Fact]
        public async Task GetUserById_WithInvalidId_ReturnsNotFound()
        {
           Guid userUuid = Guid.NewGuid();

           _userFacadeMock
               .Setup(service => service.GetUser(It.IsAny<Guid>()))
               .ReturnsAsync(null as User);

           UserController controller = new(_userFacadeMock.Object);

           var result = await controller.GetUser(userUuid);

           var okResult = Assert.IsType<ActionResult<User>>(result);
           var statusCode = Assert.IsType<NotFoundResult>(okResult.Result);
           Assert.Equal(404, statusCode.StatusCode);
        }

        [Fact]
        public async Task GetUserById_ReturnsException()
        {
           Guid userUuid = Guid.NewGuid();

           _userFacadeMock
               .Setup(service => service.GetUser(It.IsAny<Guid>()))
               .ThrowsAsync(new Exception("Unknown exception"));

           UserController controller = new(_userFacadeMock.Object);
           var result = await controller.GetUser(userUuid);

           var userActionResult = Assert.IsType<ActionResult<User>>(result);
           var statusCode = Assert.IsType<ObjectResult>(userActionResult.Result);
           Assert.Equal(500, statusCode.StatusCode);
        }
    }

    //public class PostUserMethod
    //{
    //    private readonly Mock<ILogger<UserController>> _loggerMock = new();
    //    private readonly Mock<IUserService> _userServiceMock = new();
    //    private readonly Mock<ISessionServiceFactory> _sessionServiceFactoryMock = new();

    //    [Fact]
    //    public async Task PostUserWithValidCredentialsReturnsUserCreated()
    //    {
    //        User user = new()
    //        {
    //            Id = 34,
    //            Uuid = Guid.NewGuid().ToString(),
    //            Username = "NonExistingUsernameInDb",
    //            Email = "NonExistingEmailInDb@mail.com",
    //            Password = "NonExistingUsernamesPassword123",
    //            ConfirmPassword = "NonExistingUsernamesPassword123"
    //        };

    //        _userServiceMock
    //            .Setup(service => service.AddUser(It.IsAny<User>()))
    //            .ReturnsAsync(user);

    //        UserController controller = new(_userServiceMock.Object);

    //        var result = await controller.Post(user);

    //        var okResult = Assert.IsType<CreatedAtActionResult>(result.Result);
    //        Assert.NotNull(okResult);
    //        Assert.Equal(201, okResult.StatusCode);

    //        var addedUserValue = Assert.IsType<User>(okResult.Value);
    //        Assert.NotNull(addedUserValue);
    //        Assert.Equal(user, addedUserValue);
    //    }

    //    [Fact]
    //    public async Task PostUserWithInvalidCredentialsReturnsBadRequest()
    //    {
    //        User user = new()
    //        {
    //            Id = 234,
    //            Uuid = Guid.NewGuid().ToString(),
    //            Username = null,
    //            Email = null,
    //            Password = null,
    //            ConfirmPassword = null
    //        };

    //        _userServiceMock
    //            .Setup(service => service.AddUser(It.IsAny<User>()))
    //            .ReturnsAsync(null as User);

    //        UserController controller = new(_userServiceMock.Object);

    //        controller.ModelState.AddModelError("error", "Unknown error");

    //        var result = await controller.Post(user);

    //        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
    //        Assert.NotNull(badRequestResult);
    //        Assert.IsType<BadRequestObjectResult>(badRequestResult);
    //        Assert.Equal(400, badRequestResult.StatusCode);
    //    }

    //    [Fact]
    //    public async Task PostUserWithExistingEmailReturnsConflictObjectResult()
    //    {
    //        User userToAdd = new()
    //        {
    //            Username = "username",
    //            Email = "Test_User_1@mail.com",
    //            Password = "Username1!",
    //            ConfirmPassword = "Username1!"
    //        };

    //        _userServiceMock
    //            .Setup(service => service.AddUser(It.IsAny<User>()))
    //            .ThrowsAsync(new IntegrityException("Unknown exception"));

    //        UserController controller = new(_userServiceMock.Object);

    //        var result = await controller.Post(userToAdd);

    //        var userActionResult = Assert.IsType<ConflictObjectResult>(result.Result);
    //        Assert.NotNull(userActionResult);
    //        Assert.Equal(409, userActionResult.StatusCode);
    //    }

    //    [Fact]
    //    public async Task PostUserReturnsException()
    //    {
    //        User userToAdd = new()
    //        {
    //            Username = "username",
    //            Email = "Test_User_1@mail.com",
    //            Password = "Username1!",
    //            ConfirmPassword = "Username1!"
    //        };

    //        _userServiceMock
    //            .Setup(service => service.AddUser(It.IsAny<User>()))
    //            .ThrowsAsync(new Exception("Unknown exception"));

    //        UserController controller = new(_userServiceMock.Object);

    //        var result = await controller.Post(userToAdd);

    //        var userActionResult = Assert.IsType<ActionResult<User>>(result);
    //        var statusCode = Assert.IsType<ObjectResult>(userActionResult.Result);
    //        Assert.Equal(500, statusCode.StatusCode);
    //    }
    //}

    //public class PutUserMethod
    //{
    //    private readonly Mock<ILogger<UserController>> _loggerMock = new();
    //    private readonly Mock<IUserService> _userServiceMock = new();
    //    private readonly Mock<ISessionServiceFactory> _sessionServiceFactoryMock = new();

    //    [Fact]
    //    public async Task PutUserWithValidCredentialsReturnsUser()
    //    {
    //        int userId = 1;

    //        User user = new()
    //        {
    //            Id = 1,
    //            Username = "username",
    //            Email = "email@mail.com",
    //            Password = "password",
    //            ConfirmPassword = "password"
    //        };

    //        _userServiceMock
    //            .Setup(service => service.UpdateUser(It.IsAny<int>(), It.IsAny<User>()))
    //            .ReturnsAsync(user);

    //        UserController controller = new(_userServiceMock.Object);

    //        var result = await controller.Put(userId, user);
    //        var userActionValue = Assert.IsType<ActionResult<User>>(result);
    //        Assert.Equal(user, userActionValue.Value);
    //    }

    //    [Fact]
    //    public async Task PutUserWithInvalidCredentialsReturnsBadRequestResult()
    //    {
    //        int userId = 2;

    //        User user = new() { };

    //        _userServiceMock
    //            .Setup(service => service.UpdateUser(It.IsAny<int>(), It.IsAny<User>()))
    //            .ReturnsAsync(null as User);

    //        UserController controller = new(_userServiceMock.Object);
    //        var result = await controller.Put(userId, user);

    //        var userActionResult = Assert.IsType<ActionResult<User>>(result);
    //        var statusCode = Assert.IsType<BadRequestObjectResult>(userActionResult.Result);
    //        Assert.Equal(400, statusCode.StatusCode);
    //    }

    //    [Fact]
    //    public async Task PutUserWithWrongCredentialsReturnsNotFoundObjectResult()
    //    {
    //        int userId = 1;

    //        User user = new()
    //        {
    //            Id = 1,
    //            Username = "username",
    //            Email = "email@mail.com",
    //            Password = "password",
    //            ConfirmPassword = "password"
    //        };

    //        _userServiceMock
    //            .Setup(service => service.UpdateUser(It.IsAny<int>(), It.IsAny<User>()))
    //            .ReturnsAsync(null as User);

    //        UserController controller = new(_userServiceMock.Object);

    //        var result = await controller.Put(userId, user);

    //        var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);
    //        Assert.NotNull(notFound);
    //        Assert.Equal(404, notFound.StatusCode);
    //    }

    //    [Fact]
    //    public async Task PutUserReturnsException()
    //    {
    //        int userId = 1;

    //        User user = new()
    //        {
    //            Id = 1,
    //            Username = "username",
    //            Email = "email@mail.com",
    //            Password = "password",
    //            ConfirmPassword = "password"
    //        };

    //        _userServiceMock
    //            .Setup(service => service.UpdateUser(It.IsAny<int>(), It.IsAny<User>()))
    //            .ThrowsAsync(new Exception("Unknown exception"));

    //        UserController controller = new(_userServiceMock.Object);

    //        var result = await controller.Put(userId, user);

    //        var userActionResult = Assert.IsType<ActionResult<User>>(result);
    //        var statusCode = Assert.IsType<ObjectResult>(userActionResult.Result);
    //        Assert.Equal(500, statusCode.StatusCode);
    //    }
    //}

    //public class DeleteUserMethod
    //{
    //    private readonly Mock<ILogger<UserController>> _loggerMock = new();
    //    private readonly Mock<IUserService> _userServiceMock = new();
    //    private readonly Mock<ISessionServiceFactory> _sessionServiceFactoryMock = new();

    //    [Fact]
    //    public async Task DeleteUserByIdWithValidIdReturnsUser()
    //    {
    //        int userId = 123;

    //        User user = new()
    //        {
    //            Id = 123,
    //            Uuid = Guid.NewGuid().ToString(),
    //            Username = "testuser",
    //            Email = "testuser@mail.com",
    //            Password = "testuser_password",
    //            ConfirmPassword = "testuser_password"
    //        };

    //        _userServiceMock
    //            .Setup(service => service.DeleteUser(It.IsAny<int>()))
    //            .ReturnsAsync(user);

    //        UserController controller = new(_userServiceMock.Object);
            
    //        var result = await controller.Delete(userId);
    //        var userActionValue = Assert.IsType<ActionResult<User>>(result);
    //        Assert.Equal(user, userActionValue.Value);
    //    }

    //    [Fact]
    //    public async Task DeleteUserByIdWithInvalidIdReturnsNotFound()
    //    {
    //        int userId = -1;

    //        _userServiceMock
    //            .Setup(service => service.DeleteUser(It.IsAny<int>()))
    //            .ReturnsAsync(null as User);

    //        UserController controller = new(_userServiceMock.Object);

    //        var result = await controller.Delete(userId);

    //        var okResult = Assert.IsType<ActionResult<User>>(result);
    //        var statusCode = Assert.IsType<NotFoundObjectResult>(okResult.Result);
    //        Assert.Equal(404, statusCode.StatusCode);
    //    }

    //    [Fact]
    //    public async Task GetUserByIdReturnsException()
    //    {
    //        int userId = 1233;

    //        _userServiceMock
    //            .Setup(service => service.DeleteUser(It.IsAny<int>()))
    //            .ThrowsAsync(new Exception("Unknown exception"));

    //        UserController controller = new(_userServiceMock.Object);
    //        var result = await controller.Delete(userId);

    //        var userActionResult = Assert.IsType<ActionResult<User>>(result);
    //        var statusCode = Assert.IsType<ObjectResult>(userActionResult.Result);
    //        Assert.Equal(500, statusCode.StatusCode);
    //    }
    //}
}
