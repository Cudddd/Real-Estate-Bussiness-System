using System.Threading.Tasks;

namespace BDS.Services.User
{
    public interface IUserService
    {
        public Task<bool> Authenticate(string userName, string password, bool rememberMe);
        public Task Logout();
        public Task<bool> Register();
    }
}