using EnumObjectExperiment.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnumObjectExperiment
{
    public enum SaveChangesMode
    {
        WithoutEnumeration,
        WithEnumeration
    }

    public class ExperimentContext : DbContext
    {
        public DbSet<Enumeration> Enumerations { get; set; }
        public DbSet<TaskPriority> Priorities { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskState> States { get; set; }

        public ExperimentContext()
        {
          
        }
        
        public override int SaveChanges()
        {
            return SaveChanges(SaveChangesMode.WithoutEnumeration);
        }

        public int SaveChanges(SaveChangesMode options)
        {
            // prevent Enumeration duplication
            if (options == SaveChangesMode.WithoutEnumeration)
            {
                var enumObjects = ChangeTracker.Entries<Enumeration>().Where(x => x.State == EntityState.Added);
                foreach (var item in enumObjects)
                {
                    item.State = EntityState.Unchanged;
                }

            }
            return base.SaveChanges();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=NewDictionaryTest;Trusted_Connection=True;");
            //use
            //optionsBuilder.use(
            //    @"Server=.\sqlexpress;Database=NewDictionaryTest;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TaskItem>()
                .HasOne(p => p.Priority)
                .WithMany().HasForeignKey(f=>f.PriorityId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TaskItem>()
                .HasOne(p => p.State)
                .WithMany().HasForeignKey(f => f.StateId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
