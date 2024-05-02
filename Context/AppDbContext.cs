using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace catalog.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category>? Categories {get; set;}
        public DbSet<Product>? Products {get; set;}
    }
}