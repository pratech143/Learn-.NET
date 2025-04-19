using Microsoft.EntityFrameworkCore;

namespace VideoGameAPi.Data
{
    public class VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : DbContext(options)
    {
        public DbSet<VideoGame> VideoGames => Set<VideoGame>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<VideoGame>().HasData(
                new VideoGame
                {
                    Id = 1,
                    Title = "The Legend of Zelda: Breath of the Wild",
                    Platform = "Nintendo Switch",
                    Developer = "Nintendo",
                    Publisher = "Nintendo"
                },
            new VideoGame
            {
                Id = 2,
                Title = "God of War: Ragnarok",
                Platform = "PlayStation 5",
                Developer = "Santa Monica Studio",
                Publisher = "Sony Interactive Entertainment"
            },
            new VideoGame
            {
                Id = 3,
                Title = "Halo Infinite",
                Platform = "Xbox Series X|S",
                Developer = "343 Industries",
                Publisher = "Xbox Game Studios"
            }
                );
        }
    }
}
