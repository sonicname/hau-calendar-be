using Microsoft.EntityFrameworkCore;

namespace server.Models;

public class DatabaseContext : DbContext
{
    public DatabaseContext() { }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<UserModel> Users { get; set; }
}