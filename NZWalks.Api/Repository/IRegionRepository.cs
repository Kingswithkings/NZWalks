using NZWalks.Api.Models;

namespace NZWalks.Api.Repository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid id);
        Task <Region> CreateAsync (Region region);
        Task <Region?> UpdateAsync (Region region);
        Task <Region?>DeleteAsync (Guid id);
    }
}
