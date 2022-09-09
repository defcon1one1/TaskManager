using Microsoft.EntityFrameworkCore;

namespace TaskManager.Entities
{
    internal class TaskManagerContext : DbContext
    {
        public static string connectionString = @"Data Source=localhost;Initial Catalog=TaskManager;Integrated Security=True";
        public DbSet<User> Users { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public string ConnectionString { get { return connectionString; } set { connectionString = value; } }

        public TaskManagerContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(this.ConnectionString);
        }

    }
}