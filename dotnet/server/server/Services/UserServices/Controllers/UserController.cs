using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using server.Services.UserServices.Model;
using server.Services.UserServices.Ports;
using server.Services.UserServices.Session;

namespace server.Services.UserServices.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly Singleton _singleton = Singleton.Instance;
        public UserController(IUserService userService)
        {
            // _logger = logger;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] Auth user)
        {
            try
            {
                if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                {
                    return new BadRequestObjectResult("Username and password is required.");
                }

                User? userToAuthenticate = await _userService.Login(user.Username, user.Password);

                if (userToAuthenticate == null)
                {
                    return new NotFoundObjectResult("Username or password is incorrect.");
                }

                return new OkObjectResult(userToAuthenticate);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = StatusCodes.Status500InternalServerError,
                    e.Message
                });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<Pagination>> Get(
            [FromQuery(Name = "cursor")] int cursor = 0, [FromQuery(Name = "limit")] int limit = 50)
        {
            if (limit < 50)
            {
                limit = 50;
            }

            try
            {
                (IEnumerable<User> users, int nextCursor) = await _userService.GetUsers(cursor, limit);

                if (users == null || !users.Any())
                {
                    return new NotFoundObjectResult("No users was found.");
                }

                return new OkObjectResult(new Pagination
                {
                    NextCursor = nextCursor,
                    Users = users
                });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = StatusCodes.Status500InternalServerError,
                    e.Message
                });
            }
        }

        [AllowAnonymous]
        [HttpGet("{userId:int}")]
        public async Task<ActionResult<User>> Get(int userId)
        {
            string uuid = _singleton.GetSessionUuid!;
            dynamic? session = _singleton.GetSession(uuid);
            
            if (session == null)
            {
                _singleton.SetSession(uuid,  new { users = new Dictionary<int, User>() });
            }

            session = _singleton.GetSession(uuid);

            Dictionary<int, User>? userDictionary = session?.users;

            if (userDictionary != null && userDictionary.ContainsKey(userId))
            {
                User userData = userDictionary[userId];
                return new OkObjectResult(userData);
            }
            else
            {
                try
                {
                    User? userToGet = await _userService.GetUser(userId);

                    if (userToGet == null)
                    {
                        return new NotFoundObjectResult($"User with Id {userId} was not found.");
                    }

                    // Add User to Session
                    if (userDictionary != null)
                    {
                        userDictionary[userId] = userToGet;

                        // End Session Check
                        // if (userDictionary.Count == 3)
                        // {
                        //     singleton.EndSession(uuid);
                        // }
                    }


                    return new OkObjectResult(userToGet);
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        e.Message
                    });
                }
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            try
            {
                if (user == null || !ModelState.IsValid)
                {
                    return new BadRequestObjectResult(new
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "The submitted form was invalid."
                    });
                }

                User? userToAdd = await _userService.AddUser(user);

                return CreatedAtAction(
                    nameof(Get),
                    new { user.Id },
                    userToAdd
                );
            }
            catch (IntegrityException e)
            {
                return new ConflictObjectResult(new
                {
                    Status = StatusCodes.Status409Conflict,
                    e.Message
                });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = StatusCodes.Status500InternalServerError,
                    e.Message
                });
            }
        }

        [HttpPut("{userId:int}")]
        public async Task<ActionResult<User>> Put(int userId, [FromBody] User user)
        {
            try
            {
                if (user == null || userId != user.Id)
                {
                    return new BadRequestObjectResult("Invalid user or user ID.");
                }

                User? userToUpdate = await _userService.UpdateUser(userId, user);

                if (userToUpdate == null)
                {
                    return new NotFoundObjectResult($"User with Id {userId} was not found.");
                }

                return userToUpdate;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = StatusCodes.Status500InternalServerError,
                    e.Message
                });
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
                    return new NotFoundObjectResult($"User with Id {userId} was not found.");
                }

                return userToDelete;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = StatusCodes.Status500InternalServerError,
                    e.Message
                });
            }
        }
    }
}
