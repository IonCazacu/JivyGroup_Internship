using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using server2.Services.UserServices.Model;
using server2.Services.UserServices.Ports;

namespace server2.Services.UserServices.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger < UserController > _logger;
        private readonly IUserService _userService;
        public UserController(
            ILogger < UserController > logger
            , IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task < ActionResult < User >> Login (
            [FromBody] AuthModel model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty (model.Username) ||
                    string.IsNullOrEmpty (model.Password))
                {
                    return BadRequest (
                        new { message = "Username and password is required" });
                }

                User? userToAuthenticate = await _userService.Login (
                    model.Username, model.Password);

                if (userToAuthenticate == null)
                {
                    return BadRequest (
                        new { message = "Username or password is incorrect" });
                }
                
                return Ok (userToAuthenticate);
            }
            catch (Exception e)
            {
                return StatusCode (
                    StatusCodes.Status500InternalServerError, $"{ e.Message }");
            }
        }

        // Iplement logout

        [HttpGet]
        public async Task < ActionResult < IEnumerable < User >>> Get ()
        {
            try
            {
                IEnumerable < User > users = await _userService.GetUsers ();
            
                if (users == null || !users.Any ())
                {
                    return NotFound ();
                }

                return Ok (users);
            }
            catch (Exception e)
            {
                return StatusCode (
                    StatusCodes.Status500InternalServerError, $"{ e.Message }");
            }
        }

        [HttpGet("{userId:int}")]
        public async Task < ActionResult < User >> Get (int userId)
        {
            try
            {
                User? userToGet = await _userService.GetUser (userId);

                if (userToGet == null)
                {
                    return NotFound();
                }
    
                return userToGet;
            }
            catch (Exception e)
            {
                return StatusCode (
                    StatusCodes.Status500InternalServerError, $"{ e.Message }");
            }
        }

        [HttpPost]
        public async Task < ActionResult < User >> Post (User user)
        {
            try
            {
                if (user == null || !ModelState.IsValid)
                {
                    return BadRequest ();
                }

                User? userToAdd = await _userService.AddUser (user);
                
                return CreatedAtAction(
                    nameof (Get), new { id = user.Id }, userToAdd);
            }
            catch (Exception e)
            {
                ApiResponse? response = new(500, e.Message);
                return StatusCode (
                    StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut("{userId:int}")]
        public async Task < ActionResult < User >> Put (int userId, User user)
        {
            try
            {
                if (user == null || userId != user.Id)
                {
                    return BadRequest ();
                }
                
                User? userToUpdate = await _userService.UpdateUser (
                    userId, user);

                if (userToUpdate == null)
                {
                    return NotFound ();
                }
                
                return userToUpdate;
            }
            catch (Exception e)
            {
                return StatusCode (
                    StatusCodes.Status500InternalServerError, $"{ e.Message }");
            }
        }

        [HttpDelete("{userId:int}")]
        public async Task < ActionResult < User >> Delete (int userId)
        {
            try
            {
                User? userToDelete = await _userService.DeleteUser (userId);

                if (userToDelete == null)
                {
                    return NotFound ();
                }
                
                return userToDelete;
            }
            catch (Exception e)
            {
                return StatusCode (
                    StatusCodes.Status500InternalServerError, $"{ e.Message }");
            }
        }
    }
}
