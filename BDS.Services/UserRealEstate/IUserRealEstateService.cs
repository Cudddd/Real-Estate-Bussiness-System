using System.Threading.Tasks;
using BDS.Services.Request;

namespace BDS.Services.UserRealEstate
{
    public interface IUserRealEstateService
    {
        Task<int> Create(RealEstateCreateRequest request);
    }
}