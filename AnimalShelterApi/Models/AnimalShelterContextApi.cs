using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelterApi.Models
{
    public class AnimalShelterApiContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Cat> Cats { get; set; }
        public DbSet<Dog> Dogs { get; set; }

        public AnimalShelterApiContext(DbContextOptions<AnimalShelterApiContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Dog>()
                .HasData(
                    new Dog { DogId = 1, Name = "Rex", Age = 3, Description = "A fluffy puppy, kid friendly and loves to play" },
                    new Dog { DogId = 2, Name = "Buddy", Age = 5, Description = "A big dog, loves to run and play, plays well with other puppies and kitties" }
                );

            builder.Entity<Cat>()
                .HasData(
                    new Cat { CatId = 1, Name = "Esther", Age = 9, Description = "A sassy old lady who loves to lay around and judge" },
                    new Cat { CatId = 2, Name = "Mittens", Age = 2, Description = "A young kitty, loves to play and chase toys" }
                );
        }
    }
}
