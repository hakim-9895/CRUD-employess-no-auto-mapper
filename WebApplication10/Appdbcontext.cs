using System;
using Microsoft.EntityFrameworkCore;
using WebApplication10.Modal;

namespace WebApplication10
{
    public class AppDbcontext :DbContext
    {
      public  DbSet<Employes> Employes { get; set; }
      public DbSet<Department> Department { get; set; }
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employes>()
                .HasOne(e => e.department)
                .WithMany(d => d.Employes)
                .HasForeignKey(e => e.Departmentid)
                .OnDelete(DeleteBehavior.Cascade);
        }



    }
}
