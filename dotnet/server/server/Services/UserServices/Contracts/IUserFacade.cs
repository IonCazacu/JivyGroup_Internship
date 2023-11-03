using Contracts;
using server.Services.UserServices.Entities;
using server.Services.UserServices.Entities.Authorization;

namespace server.Services.UserServices.Contracts
{
    public interface IUserFacade : IEntityOperation<User>
    {
        Task<User?> AuthenticateUser(BasicAuthorization user);
        Task<Pagination?> GetUsers(int cursor, int limit);
        Task<User?> GetUser(Guid userId);
    }
}
