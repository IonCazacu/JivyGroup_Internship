using server2.Services.UserServices.Model;

namespace server2.Services.UserServices.Ports
{
    public interface IUserService
    {
        Task < User? > Login (string username, string password);
        // Task<User?> Logout();
        Task < IEnumerable < User >> GetUsers ();
        Task < User? > GetUser (int userId);
        Task < User? > AddUser (User user);
        Task < User? > UpdateUser (int userId, User user);
        Task < User? > DeleteUser (int userId);
    }
}
