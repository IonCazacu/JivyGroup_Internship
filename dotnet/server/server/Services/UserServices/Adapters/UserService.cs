using Microsoft.EntityFrameworkCore;
using server.Services.UserServices.Data;
using server.Services.UserServices.Model;
using server.Services.UserServices.Ports;

namespace server.Services.UserServices.Adapters
{
    public class UserService : IUserService
    {
        private readonly UserContext userContext;

        public UserService(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public async Task<User?> Login(string username, string password)
        {
            try
            {
                User? userToLogin = await userContext.Users.FirstOrDefaultAsync(
                    u => u.Username == username && u.Password == password);

                if (userToLogin == null)
                {
                    return null;
                }

                return userToLogin;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<(IEnumerable<User>, int)> GetUsers(int cursor, int limit)
        {
            int maxId = await userContext.Users.MaxAsync(u => u.Id);

            IEnumerable<User> users = await userContext.Users
                .Select(u => new User
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email
                })
                .Where(u => u.Id > cursor && u.Id <= maxId)
                .OrderBy(u => u.Id)
                .Take(limit)
                .ToListAsync();

            int nextCursor = users.Any() ? users.Last().Id : -1;

            return (users.Take(limit), nextCursor);
        }

        public async Task<User?> GetUser(int userId)
        {
            try
            {
                User? userToGet = await userContext.Users.FindAsync(userId);
                
                if (userToGet == null)
                {
                    return null;
                }
                
                return userToGet;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<User?> AddUser(User user)
        {
            try
            {
                User? userToAdd = userContext.Users.Where(u => u.Email == user.Email).FirstOrDefault();

                if (userToAdd != null)
                {
                    throw new IntegrityException("That email is already taken");
                }

                userContext.Users.Add(user);
                await userContext.SaveChangesAsync();
                
                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<User?> UpdateUser(int userId, User user)
        {
            try
            {
                User? userToUpdate = await userContext.Users.FindAsync(userId);

                if (userToUpdate == null)
                {
                    return null;
                }

                userContext.Entry(userToUpdate).CurrentValues.SetValues(user);
                await userContext.SaveChangesAsync();

                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<User?> DeleteUser(int userId)
        {
            try
            {
                User? userToDelete = await userContext.Users.FindAsync(userId);

                if (userToDelete == null)
                {
                    return null;
                }

                userContext.Users.Remove(userToDelete);
                await userContext.SaveChangesAsync();

                return userToDelete;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
