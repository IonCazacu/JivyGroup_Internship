using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using server.Services.UserServices.Model;
using server.Services.UserServices.Ports;

namespace server.Services.UserServices.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            // _logger = logger;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] AuthModel model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                {
                    return new BadRequestObjectResult("Username and password is required");
                }

                User? userToAuthenticate = await _userService.Login(
                    model.Username,
                    model.Password
                    );

                if (userToAuthenticate == null)
                {
                    return new NotFoundObjectResult("Username or password is incorrect");
                }

                return new OkObjectResult(userToAuthenticate);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            try
            {
                IEnumerable<User> users = await _userService.GetUsers();

                if (users == null || !users.Any())
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(users);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{userId:int}")]
        public async Task<ActionResult<User>> Get(int userId)
        {
            try
            {
                User? userToGet = await _userService.GetUser(userId);

                if (userToGet == null)
                {
                    return new NotFoundObjectResult($"User with Id {userId} was not found");
                }

                return new OkObjectResult(userToGet);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            try
            {
                // if (user == null || !ModelState.IsValid)
                // {
                //     return BadRequest(
                //         new
                //         {
                //             Status = StatusCodes.Status400BadRequest,
                //             Message = "The submitted form was invalid."
                //         }
                //     );
                // }

                User? userToAdd = await _userService.AddUser(user);

                return CreatedAtAction(
                    nameof(Get),
                    new { Id = user.Id },
                    new
                    {
                        userToAdd,
                        Message = "Your account has been successfully created."
                    }
                );
            }
            catch (ArgumentNullException ex)
            {
                return new BadRequestObjectResult(new
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                });
            }
            catch (IntegrityException ex)
            {
                return new ConflictObjectResult(new
                {
                    Status = StatusCodes.Status409Conflict,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
            }
        }

        [HttpPut("{userId:int}")]
        public async Task<ActionResult<User>> Put(int userId, User user)
        {
            try
            {
                if (user == null || userId != user.Id)
                {
                    return new BadRequestResult();
                }

                User? userToUpdate = await _userService.UpdateUser(userId, user);

                if (userToUpdate == null)
                {
                    return new NotFoundObjectResult($"User with Id {userId} was not found");
                }

                return userToUpdate;
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{userId:int}")]
        public async Task<ActionResult<User>> Delete(int userId)
        {
            try
            {
                User? userToDelete = await _userService.DeleteUser(userId);

                if (userToDelete == null)
                {
                    return new NotFoundObjectResult($"User with Id {userId} was not found");
                }

                return userToDelete;
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
