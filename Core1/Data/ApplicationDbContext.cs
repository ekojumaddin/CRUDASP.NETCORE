using System;
using System.Collections.Generic;
using System.Text;
using Core1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){ }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ModelMap(modelBuilder.Entity<Supplier>());
        }
        public DbSet<Core1.Models.Supplier> Supplier { get; set; }
    }
}
