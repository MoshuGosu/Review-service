using Microsoft.EntityFrameworkCore;
using Review_service_WebAPI.Models;

namespace Review_service_WebAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<BookReview> BookReviews { get; set; }
}