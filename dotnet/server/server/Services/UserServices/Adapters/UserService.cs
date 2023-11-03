using server.Services.UserServices.Contracts;
using server.Services.UserServices.Entities;
using server.Services.UserServices.Entities.Authorization;

namespace server.Services.UserServices.Adapters
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> AuthenticateUser(BasicAuthorization user)
        {
            try
            {
                User? userToLogin = await _userRepository.AuthenticateUser(user);

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
                var result = await _userRepository.GetUsers(cursor, limit);

                if (
                    result == null ||
                    result.Users == null ||
                    result.NextCursor == -1
                ) {
                    return null;
                }
                
                IEnumerable<User> users = result.Users;
                long nextCursor = result.NextCursor;
                bool hasNextCursor = result.HasNextCursor;

                return new Pagination
                {
                    Users = users,
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
                User? userToGet = await _userRepository.GetUser(userUuid);
                
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
               return await _userRepository.AddAsync(user);
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
               return await _userRepository.UpdateAsync(user);
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
               return await _userRepository.DeleteAsync(user);
           }
           catch (Exception)
           {
               throw;
           }
        }
    }
}
