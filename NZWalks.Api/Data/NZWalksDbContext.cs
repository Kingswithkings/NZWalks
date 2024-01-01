using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NZWalks.Api.Models;
using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Data
{
    public class NZWalksDbContext : DbContext 
    {
        public NZWalksDbContext(DbContextOptions dbContextoptions) : base(dbContextoptions)
        {

        }

        public DbSet<Difficulty> difficulties { get; set; }
        public DbSet<Region> regions { get; set; }
        public DbSet<Walk> walks { get; set; }

    }
}
