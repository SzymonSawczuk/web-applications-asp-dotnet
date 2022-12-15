using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab10.Models;

namespace lab10.Data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Article> Article { get; set; }
    }
}
