using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Models;

namespace TaskManagementApi.Data
{
    public class AppDbContext : DbContext
    {
        // DbContextOptions - an internal API that supports the Entity Framework Core infrastructure
        // and not subject to the same compatibility standards as public APIs
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) 
        { }
        //DbSet - a collection of all entities in the database of a given type
        public DbSet<TaskItem> Tasks => Set<TaskItem>();
    }
}
