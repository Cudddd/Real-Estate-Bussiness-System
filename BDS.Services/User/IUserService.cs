using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BDS.Data.Entities;
using BDS.Services.Model;

namespace BDS.Services.User
{
    public interface IUserService
    {
        public Task<bool> Authenticate(string userName, string password, bool rememberMe);
        public Task Logout();
        public Task<bool> Register();

        public Task<Data.Entities.User> GetCurrentUser(ClaimsPrincipal User);

        public Task<List<UserRealEstateModel>> GetAllUserRealEstate(long userId);
        public Task<UserRealEstateModel> GetUserRealEstateById(long id);
    }
}