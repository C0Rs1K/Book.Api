using Book.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Book.Domain.Models.Abstract;

namespace Book.Infrastructure.Database;

public class BookDbContext(DbContextOptions<BookDbContext> options) : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<AuthorModel> Authors { get; set; }
    public DbSet<BookModel> Books { get; set; }
    public DbSet<GenreModel> Genres { get; set; }
    public DbSet<CountryModel> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(BaseModel)) ?? throw new InvalidOperationException());
    }
}