using Microsoft.EntityFrameworkCore;
using server2.Services.UserServices.Data;
using server2.Services.UserServices.Model;
using server2.Services.UserServices.Ports;

namespace server2.Services.UserServices.Adapters
{
    public class UserService : IUserService
    {
        private readonly UserContext userContext;

        public UserService (UserContext userContext)
        {
            this.userContext = userContext;
        }

        public async Task < User? > Login (string username, string password)
        {
            try
            {
                User? userToLogin =
                    await userContext.Users.FirstOrDefaultAsync (
                        u => u.Username == username && u.Password == password);

                if (userToLogin == null)
                {
                    return null;
                }

                // password hash verifications

                return userToLogin;
            }
            catch (Exception e)
            {
                throw new Exception ($"{ e.Message }");
            }
        }

        public async Task < IEnumerable < User >> GetUsers ()
        {
            return await userContext.Users.Select (
                u => new User {
                    Id = u.Id, Username = u.Username, Email = u.Email
                    }).ToListAsync ();
        }

        public async Task < User? > GetUser (int userId)
        {
            try
            {
                User? userToGet = await userContext.Users.FindAsync (userId);
                
                if (userToGet == null)
                {
                    return null;
                }
                
                return userToGet;
            }
            catch (Exception e)
            {
                throw new Exception ($"{ e.Message }");
            }
        }

        public async Task < User? > AddUser (User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException (
                        nameof (user), "User object cannot be null");
                }

                User? userToAdd = userContext.Users.Where (
                    u => u.Email == user.Email).FirstOrDefault ();

                if (userToAdd != null)
                {
                    throw new IntegrityException (
                        $"User with email { user?.Email } is already registered");
                }

                userContext.Users.Add (user);
                await userContext.SaveChangesAsync ();
                
                return user;
            }
            catch (Exception e)
            {
                throw new Exception ($"{ e.Message }");
            }
        }

        public async Task < User? > UpdateUser (int userId, User user)
        {
            try
            {
                User? userToUpdate = await userContext.Users.FindAsync (userId);

                if (userToUpdate == null)
                {
                    return null;
                }

                userContext.Entry (userToUpdate).CurrentValues.SetValues (user);
                await userContext.SaveChangesAsync ();

                return user;
            }
            catch (Exception e)
            {
                throw new Exception ($"{ e.Message }");
            }
        }

        public async Task < User? > DeleteUser (int userId)
        {
            try
            {
                User? userToDelete = await userContext.Users.FindAsync (userId);

                if (userToDelete == null)
                {
                    return null;
                }

                userContext.Users.Remove (userToDelete);
                await userContext.SaveChangesAsync ();

                return userToDelete;
            }
            catch (Exception e)
            {
                throw new Exception ($"{ e.Message }");
            }
        }
    }
}
