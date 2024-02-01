using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Data;
using NZWalks.Api.Models;

namespace NZWalks.Api.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public Task<Region?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
           var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x=> x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            // Check if the region with the specified id exists
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            // If the region does not exist, return null
            if (existingRegion == null)
            {
                return null;
            }

            // Remove the region from the database
            dbContext.Regions.Remove(existingRegion);

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            // Return the deleted region
            return existingRegion;
        }
    }
}
