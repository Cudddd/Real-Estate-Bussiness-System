using System.Threading.Tasks;

namespace BDS.Services.User
{
    public interface IUserService
    {
        public Task<string> Authenticate(string userName, string password, bool rememberMe);
        public Task<bool> Register();
    }
}