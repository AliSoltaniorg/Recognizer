
using Microsoft.EntityFrameworkCore;
using Presentation.Models;

namespace Presentation.DbContext;

public class RecognizerContext : Microsoft.EntityFrameworkCore.DbContext
{
  public RecognizerContext(DbContextOptions<RecognizerContext> options)
    : base(options)
  { }
  public DbSet<User> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>().HasKey(prop => prop.Guid);
    modelBuilder.Entity<User>().Property(prop => prop.Id).ValueGeneratedOnAdd();
    modelBuilder.Entity<User>().Ignore(prop => prop.FullName);
    //base.OnModelCreating(modelBuilder);
  }
}
