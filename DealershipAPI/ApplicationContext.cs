using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DealershipAPI
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Entities.BodyEntity> Body { get; set; }
        public DbSet<Entities.BrandEntity> Brand { get; set; }
        public DbSet<Entities.ModelEntity> Model { get; set; }
        public DbSet<Entities.CarEntity> Car { get; set; }

        public DbSet<Entities.UserEntity> User { get; set; }
        public DbSet<Entities.ContactEntity> Contact { get; set; }


    }
}
