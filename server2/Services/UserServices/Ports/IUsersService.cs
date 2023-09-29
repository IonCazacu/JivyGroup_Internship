using server2.Services.UserServices.Models;

namespace server2.Services.UserServices.Ports
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User?> GetUser(int userId);
        Task<User?> AddUser(User user);
        Task<User?> UpdateUser(int userId, User user);
        Task<User?> DeleteUser(int userId);
    }
}
