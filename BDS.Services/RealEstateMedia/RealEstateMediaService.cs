using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Data.EF;

namespace BDS.Services.RealEstateMedia
{
    public class RealEstateMediaService : IRealEstateMediaService
    {
        private readonly BdsDbContext _context;

        public RealEstateMediaService(BdsDbContext context)
        {
            _context = context;
        }
        public Task<int> Create(Data.Entities.RealEstateMedia realEstateMedia)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Update(Data.Entities.RealEstateMedia realEstateMedia)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Delete(long realEstateMediaId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> DeleteRange(List<Data.Entities.RealEstateMedia> realEstateMedia)
        {
            _context.RealEstateMedia.RemoveRange(realEstateMedia);
            return await _context.SaveChangesAsync();
        }

        public Task<Data.Entities.RealEstateMedia> GetById(long realEstateMediaId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Data.Entities.RealEstateMedia>> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}