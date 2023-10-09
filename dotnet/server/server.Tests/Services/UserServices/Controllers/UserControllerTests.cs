using Microsoft.AspNetCore.Mvc;
using Moq;
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

            var controller = new Services.UserServices.Controllers.UserController(_userServiceMock.Object);
            var result = await controller.Login(authModel);

            var okResult = Assert.IsType<ActionResult<User>>(result);

            var objectResult = okResult.Result as OkObjectResult;
            
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);

            var returnedUser = Assert.IsType<User>(objectResult.Value as User);

            Assert.NotNull(returnedUser);
            Assert.Equal(1, returnedUser.Id);
            Assert.Equal("testuser", returnedUser.Username);
            Assert.Equal("testuser_password", returnedUser.Password);
            Assert.Equal(user, returnedUser);
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
                .ReturnsAsync(null as User); ;

            var controller = new Services.UserServices.Controllers.UserController(_userServiceMock.Object);
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

            var controller = new Services.UserServices.Controllers.UserController(_userServiceMock.Object);
            var result = await controller.Login(authModel);

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<NotFoundObjectResult>(userActionResult.Result);
            Assert.Equal(404, statusCode.StatusCode);
        }

        [Fact]
        public async Task LoginUserThrowsException()
        {
            _userServiceMock
                .Setup(service => service.Login(
                    It.IsAny<string>(),
                    It.IsAny<string>()
                    )
                )
                .ThrowsAsync(new Exception("Unknown exception"));

            var controller = new Services.UserServices.Controllers.UserController(_userServiceMock.Object);

            var result = await controller.Login(new AuthModel
            {
                Username = "test_username",
                Password = "test_password"
            });

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<StatusCodeResult>(userActionResult.Result);
            Assert.Equal(500, statusCode.StatusCode);
        }
    }
    
    public class GetUsersMethod
    {
        private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();

        [Fact]
        public async Task GetAllUsersThrowsException()
        {
            _userServiceMock
                .Setup(service => service.GetUsers())
                .ThrowsAsync(new Exception("Unknown exception"));

            var controller = new Services.UserServices.Controllers.UserController(_userServiceMock.Object);
            var result = await controller.Get();

            var enumUserActionResult = Assert.IsType<ActionResult<IEnumerable<User>>>(result);
            var statusCode = Assert.IsType<StatusCodeResult>(enumUserActionResult.Result);
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

            var controller = new Services.UserServices.Controllers.UserController(_userServiceMock.Object);
            var result = await controller.Get(userId);

            var okResult = Assert.IsType<ActionResult<User>>(result);

            var objectResult = okResult.Result as OkObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);

            var returnedUser = Assert.IsType<User>(objectResult.Value as User);

            Assert.NotNull(returnedUser);
            Assert.Equal(user, returnedUser);
        }

        [Fact]
        public async Task GetUserByIdWithInvalidIdReturnsNotFound()
        {
            int userId = -1;

            _userServiceMock
                .Setup(service => service.GetUser(userId))
                .ReturnsAsync(null as User);
            
            var controller = new Services.UserServices.Controllers.UserController(_userServiceMock.Object);

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

            var controller = new Services.UserServices.Controllers.UserController(_userServiceMock.Object);
            var result = await controller.Get(userId);

            var userActionResult = Assert.IsType<ActionResult<User>>(result);
            var statusCode = Assert.IsType<StatusCodeResult>(userActionResult.Result);
            Assert.Equal(500, statusCode.StatusCode);
        }
    }
}
