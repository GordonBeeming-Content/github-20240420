namespace SampleApp.Database;

public partial class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
  {
  }

  public virtual DbSet<Contact> Contacts { get; set; }
}
