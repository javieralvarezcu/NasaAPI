using Microsoft.EntityFrameworkCore;
using NasaApi.Models.DTO;

namespace NasaApi.Persistence.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public DbSet<NearEarthObjectDTO> near_earth_objects { get; set; }
    }
}
