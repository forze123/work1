using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work1.Models;

namespace work1.Models
{
    public class SykContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public SykContext(DbContextOptions<SykContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
