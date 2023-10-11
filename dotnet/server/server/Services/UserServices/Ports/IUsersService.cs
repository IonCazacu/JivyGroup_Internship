using server.Services.UserServices.Model;

namespace server.Services.UserServices.Ports
{
    public interface IUserService
    {
        Task<User?> Login (string username, string password);
        Task<IEnumerable<User>> GetUsers ();
        Task<IEnumerable<User>> GetUsers (int cursor, int pageSize);
        Task<User?> GetUser (int userId);
        Task<User?> AddUser (User user);
        Task<User?> UpdateUser (int userId, User user);
        Task<User?> DeleteUser (int userId);
    }
}
