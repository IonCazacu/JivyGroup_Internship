using Microsoft.EntityFrameworkCore;
using server.Services.SessionServices.Contracts;
using server.Services.UserServices.Context;
using server.Services.UserServices.Contracts;
using server.Services.UserServices.Entities;
using server.Services.UserServices.Entities.Authorization;

namespace server.Services.UserServices.Adapters
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;
        private readonly ISessionServiceFactory _sessionServiceFactory;

        public UserRepository(
            UserContext userContext,
            ISessionServiceFactory sessionServiceFactory
        )
        {
            _userContext = userContext;
            _sessionServiceFactory = sessionServiceFactory;
        }

        public async Task<User?> AuthenticateUser(BasicAuthorization user)
        {
            try
            {
                User? userToLogin = await _userContext
                    .Users
                    .FirstOrDefaultAsync(u =>
                        u.Username == user.Username &&
                        u.Password == user.Password
                    );

                if (userToLogin == null)
                {
                    return null;
                }

                return userToLogin;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Pagination?> GetUsers(int cursor, int limit)
        {
            try
            {
                long maxId = await _userContext.Users.MaxAsync(u => u.Id);

                IEnumerable<User> users = await _userContext.Users
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

                long nextCursor = users.Any() ? users.Last().Id : -1;
                bool hasNextCursor = nextCursor != maxId;

                return new Pagination
                {
                    Users = users.Take(limit),
                    NextCursor = nextCursor,
                    HasNextCursor = hasNextCursor
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User?> GetUser(Guid userUuid)
        {
            try
            {
                User? userToGet = await _userContext.Users
                    .Where(u => u.Uuid == userUuid.ToString())
                    .FirstOrDefaultAsync();

                if (userToGet == null)
                {
                    return null;
                }

                return userToGet;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> AddAsync(User user)
        {
            try
            {
                User? userToAdd = _userContext.Users.Where(u => u.Email == user.Email).FirstOrDefault();

                if (userToAdd != null)
                {
                    throw new IntegrityException("That email is already taken");
                }

                _userContext.Users.Add(user);
                await _userContext.SaveChangesAsync();

                return user;
            }
            catch (IntegrityException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User?> UpdateAsync(User user)
        {
            try
            {
                User? userToUpdate = await _userContext.Users.FindAsync(user.Id);

                if (userToUpdate == null)
                {
                    return null;
                }

                _userContext.Entry(userToUpdate).CurrentValues.SetValues(user);
                await _userContext.SaveChangesAsync();

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User?> DeleteAsync(User user)
        {
            try
            {
                User? userToDelete = await _userContext.Users.FindAsync(user.Id);

                if (userToDelete == null)
                {
                    return null;
                }

                _userContext.Users.Remove(userToDelete);
                await _userContext.SaveChangesAsync();

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
