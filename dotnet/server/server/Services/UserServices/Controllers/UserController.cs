using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using server.Services.UserServices.Contracts;
using server.Services.UserServices.Entities;
using server.Services.UserServices.Entities.Authorization;

namespace server.Services.UserServices.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // private readonly ILogger<UserController> _logger;
        private readonly IUserFacade _userFacade;
        public UserController(IUserFacade userFacade/*, ILogger<UserController> logger*/)
        {
            // _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userFacade = userFacade ?? throw new ArgumentNullException(nameof(userFacade));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> AuthenticateUser(
            [FromBody] BasicAuthorization user
        ) {
            try
            {
                if (
                    user == null || 
                    string.IsNullOrEmpty(user.Username) ||
                    string.IsNullOrEmpty(user.Password)
                ) {
                    return NotFound();
                }

               User? userToAuthenticate = await _userFacade.AuthenticateUser(user);

               if (userToAuthenticate == null)
               {
                   return NotFound();
               }

               return Ok(userToAuthenticate);
           }
           catch (Exception e)
           {
               return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
           }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Pagination?>> GetUsers(
           [FromQuery(Name = "cursor")] int cursor = 0,
           [FromQuery(Name = "limit")] int limit = 50
        ) {
            if (limit < 50)
            {
                limit = 50;
            }

            try
            {
                var result = await _userFacade.GetUsers(cursor, limit);

                if (
                    result == null ||
                    result.Users == null ||
                    result.NextCursor == -1
                ) {
                    return NotFound();
                }

                IEnumerable<User> users = result.Users;
                long nextCursor = result.NextCursor;
                bool hasNextCursor = result.HasNextCursor;

                return Ok(new Pagination
                {
                    NextCursor = nextCursor,
                    Users = users,
                    HasNextCursor = hasNextCursor
                });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{userUuid:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<User?>> GetUser([FromRoute] Guid userUuid)
        {
            try
            {   
                User? userToGet = await _userFacade.GetUser(userUuid);

                if (userToGet == null)
                {
                    return NotFound();
                }

                return Ok(userToGet);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
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

                User? userToAdd = await _userFacade.AddAsync(user);
                
                return CreatedAtAction(
                    nameof(GetUser),
                    new { userUuid = user.Uuid },
                    user
                );
            }
            catch (IntegrityException e)
            {
                return Conflict(new
                {
                    Status = StatusCodes.Status409Conflict,
                    e.Message
                });
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    e.Message
                );
            }
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<ActionResult<User>> UpdateUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Invalid user.");
                }

                User? userToUpdate = await _userFacade.UpdateAsync(user);

                if (userToUpdate == null)
                {
                    return NotFound($"User with Id {user.Id} was not found.");
                }

                return userToUpdate;
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    e.Message
                );
            }
        }
        
        [HttpDelete]
        [AllowAnonymous]
        public async Task<ActionResult<User>> DeleteUser([FromBody] User user)
        {
            try
            {
                User? userToDelete = await _userFacade.DeleteAsync(user);

                if (userToDelete == null)
                {
                    return NotFound($"User with Id {user.Id} was not found.");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    e.Message
                );
            }
        }
    }
}
