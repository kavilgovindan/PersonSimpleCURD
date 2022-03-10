using Microsoft.EntityFrameworkCore;

namespace PersonSimpleCURD.Models
{
    public class PersonDbContext:DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options):base(options)
        { }

        public DbSet<PersonModel> persons { get; set; }

    }
}
