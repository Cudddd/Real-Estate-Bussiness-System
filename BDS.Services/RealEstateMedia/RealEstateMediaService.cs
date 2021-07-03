using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace BDS.Services.RealEstateMedia
{
    public class RealEstateMediaService : IRealEstateMediaService
    {
        private readonly BdsDbContext _context;
        private readonly IStorageService _storageService;

        public RealEstateMediaService(BdsDbContext context,IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<int> Create(Data.Entities.RealEstateMedia realEstateMedia)
        {
            await _context.RealEstateMedia.AddAsync(realEstateMedia);
            return await _context.SaveChangesAsync();
        }

        public Task<int> Update(Data.Entities.RealEstateMedia realEstateMedia)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Delete(long realEstateMediaId)
        {
            var entity = await _context.RealEstateMedia.FirstOrDefaultAsync(x=>x.RealEstateId == realEstateMediaId);

            if (entity != null)
            {
                await _storageService.DeleteFileAsync(entity.Path);

                _context.RealEstateMedia.Remove(entity);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> DeleteRange(List<Data.Entities.RealEstateMedia> realEstateMedia)
        {
            foreach (var item in realEstateMedia)
            {
                if(item != null)
                    await _storageService.DeleteFileAsync(item.Path);
            }
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