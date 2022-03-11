using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterProject.Models
{
    public class WaterProjectContext : DbContext
    {
        public WaterProjectContext()
        {

        }
        public WaterProjectContext(DbContextOptions<WaterProjectContext> options)
            : base(options)
        {

        }
        public virtual DbSet<Project>Projects { get; set; }
        public DbSet<Donation> Donations { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlite("Data Source = WaterProject.sqlite");
        //    }
        //}
        //protected override void OnModelCreating()
    }
}
