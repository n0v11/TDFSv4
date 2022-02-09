using System;
using Microsoft.EntityFrameworkCore;

namespace TDFSv4.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Type> Types { get; set; }
        public DbSet<Client> Clients { get; set; } 
        public DbSet<Founder> Founders { get; set; }
        

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var t = new Type {Id = 1, Name = "ЮЛ" };
            builder.Entity<Type>().HasData(t);
            //builder.Entity<Type>().HasData(t);
                
            builder.Entity<Client>().HasData(
                new Client { Id = 1, Name = "ООО Аленка", TypeId = t.Id, Tin = 9998130573, CreationDate = new DateTime(1996, 04, 03, 3, 30, 55), UpdateDate = DateTime.Now }
            );

        // https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
        }
    }
}
