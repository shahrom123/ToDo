

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {


    }
    public DbSet<User> Users { get; set; } 
    public DbSet<TodoTask> TodoTasks { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<OrderItem>().HasKey(bc => new { bc.OrderId, bc.ProductId});

        base.OnModelCreating(modelBuilder);
    }
}

