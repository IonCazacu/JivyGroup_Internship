using Microsoft.AspNetCore.Mvc;
using server2.Services.UserServices.Models;
using server2.Services.UserServices.Ports;

namespace server2.Services.UserServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(
            ILogger<UserController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            try
            {
                IEnumerable<User>? users = await _userService.GetUsers();
            
                if (users == null || !users.Any())
                {
                    return NotFound();
                }

                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e.Message}");
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
                    return NotFound();
                }
    
                return userToGet;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e.Message}");
            }    
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                User? userToAdd = await _userService.AddUser(user);
                
                return CreatedAtAction(
                    nameof(Get), new { id = user.Id }, userToAdd);
            }
            catch (IntegrityException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e.Message}");
            }
        }

        [HttpPut("{userId:int}")]
        public async Task<ActionResult<User>> Put(int userId, User user)
        {
            try
            {
                if (user == null || userId != user.Id)
                {
                    return BadRequest();
                }
                
                User? userToUpdate = await _userService.UpdateUser(userId, user);

                if (userToUpdate == null)
                {
                    return NotFound();
                }
                
                return userToUpdate;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e.Message}");
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
                    return NotFound();
                }
                
                return userToDelete;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e.Message}");
            }
        }
    }
}