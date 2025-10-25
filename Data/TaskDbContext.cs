using Basic_Task_Manager.Models;
using Microsoft.EntityFrameworkCore;

namespace Basic_Task_Manager.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; } = null!;
    }
}