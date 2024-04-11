using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DealershipAPI
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        //public DbSet<Entities.Body> Bodies { get; set; }
        public DbSet<Entities.BrandEntity> Brand { get; set; }
       // public DbSet<Entities.Model> Models { get; set; }
       // public DbSet<Entities.Car> Cars { get; set; }
        //public DbSet<Entities.Image> Images { get; set; }


    }
}
