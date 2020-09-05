
using Microsoft.EntityFrameworkCore;
using Receive_API.Models;

namespace Receive_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<User> User {get;set;}
        public DbSet<Category> Category {get;set;}
        public DbSet<Department> Department {get;set;}
        public DbSet<Product> Product {get;set;}
        public DbSet<Receive> Receive {get;set;}
        public DbSet<Role> Role {get;set;}
    }
}