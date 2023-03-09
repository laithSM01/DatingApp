using System;

using Microsoft.EntityFrameworkCore;

using DatingApp.API.Models; //to read Models
namespace DatingApp.Data
{
    public class DataContext:DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


       public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }

    }
}
