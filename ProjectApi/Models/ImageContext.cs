using Microsoft.EntityFrameworkCore;

namespace ProjectApi.Models;

public class ImageContext : DbContext
{
    public ImageContext(DbContextOptions<ImageContext> options): base(options)
    {
    }

    public DbSet<Image> Images { get; set; } = null!;
}