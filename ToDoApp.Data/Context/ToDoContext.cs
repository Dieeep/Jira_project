using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Models;

namespace ToDoApp.Data.Context
{
    public class ToDoContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Models.TaskItem> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseNpgsql("Host=localhost;Database=ToDoApp;Username=postgres;Password=07082004");
            base.OnConfiguring(optionsBuilder);

        }


        // model configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>()
                .HasKey(b => b.Id);
            modelBuilder.Entity<Board>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<Status>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<ToDoApp.Data.Models.TaskItem>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<ToDoApp.Data.Models.TaskItem>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("now()");
            modelBuilder.Entity<ToDoApp.Data.Models.TaskItem>()
                .HasOne(t => t.Board)
                .WithMany(b => b.Tasks)
                .HasForeignKey(t => t.BoardId);
            modelBuilder.Entity<ToDoApp.Data.Models.TaskItem>()
                .HasOne(t => t.Status)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.StatusId);
            modelBuilder.Entity<ToDoApp.Data.Models.TaskItem>()
                .HasOne(t => t.Assignee)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AssigneeId);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

        }
    }
}
