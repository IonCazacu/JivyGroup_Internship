using server.Services.UserServices.Contracts;
using server.Services.SessionServices.Contracts;
using server.Services.UserServices.Entities.Authorization;
using server.Services.UserServices.Entities;

namespace server.Services.UserServices.Adapters
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserService _userService;
        private readonly ISessionServiceFactory _sessionServiceFactory;
        
        public UserFacade(
            IUserService userService,
            ISessionServiceFactory sessionServiceFactory
        ) {
            _userService = userService;
            _sessionServiceFactory = sessionServiceFactory;
        }

        public async Task<User?> AuthenticateUser(BasicAuthorization user)
        {
            return await _userService.AuthenticateUser(user);
        }

        public async Task<Pagination?> GetUsers(int cursor, int limit)
        {
            return await _userService.GetUsers(cursor, limit);
        }

        public async Task<User?> GetUser(Guid userUuid)
        {
            string uuid = _sessionServiceFactory.GetSessionService().GetSessionUuid!;
            dynamic? session = _sessionServiceFactory.GetSessionService().GetSession(uuid);

            if (session == null)
            {
                var initialSessionData = new
                {
                    users = new Dictionary<Guid, User>()
                };

                _sessionServiceFactory.GetSessionService().SetSession(uuid, initialSessionData);

                session = initialSessionData;
            }

            Dictionary<Guid, User>? userDictionary = session?.users;

            if (userDictionary != null && userDictionary.TryGetValue(userUuid, out User? userToGet))
            {
                return userToGet;
            }

            try
            {
                userToGet = await _userService.GetUser(userUuid);

                if (userToGet == null)
                {
                    return null;
                }

                // Add User to Session
                if (userDictionary != null)
                {
                    userDictionary[userUuid] = userToGet;
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
            return await _userService.AddAsync(user);
        }

        public async Task<User?> UpdateAsync(User user)
        {
            return await _userService.UpdateAsync(user);
        }

        public async Task<User?> DeleteAsync(User user)
        {
            return await _userService.DeleteAsync(user);
        }
    }
}
