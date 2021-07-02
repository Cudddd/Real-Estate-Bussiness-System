using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDS.Services.RealEstateMedia
{
    using Data.Entities;
    public interface IRealEstateMediaService
    {
        Task<int> Create(RealEstateMedia realEstateMedia);
        Task<int> Update(RealEstateMedia realEstateMedia);
        Task<int> Delete(long realEstateMediaId);
        Task<int> DeleteRange(List<RealEstateMedia> realEstateMedia);
        Task<RealEstateMedia> GetById(long realEstateMediaId);
        Task<List<RealEstateMedia>> GetAll();
    }
}