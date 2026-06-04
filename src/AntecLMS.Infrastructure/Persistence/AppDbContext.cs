using AntecLMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AntecLMS.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options) { }

  public DbSet<User> Users => Set<User>();
  public DbSet<Course> Courses => Set<Course>();
  public DbSet<Group> Groups => Set<Group>();
  public DbSet<Teacher> Teachers => Set<Teacher>();
  public DbSet<Student> Students => Set<Student>();
  public DbSet<GroupStudent> GroupStudents => Set<GroupStudent>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    base.OnModelCreating(modelBuilder);
  }
}
