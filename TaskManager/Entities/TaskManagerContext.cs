using Microsoft.EntityFrameworkCore;

namespace TaskManager.Entities
{
    internal class TaskManagerContext : DbContext
    {
        internal static string connectionString = @"Data Source=localhost;Initial Catalog=TaskManager;Integrated Security=True";

        /// <summary>
        /// DbSet for Users table in mysql database
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// DbSet for Statuses table in mysql database
        /// </summary>
        public DbSet<Status> Statuses { get; set; }

        /// <summary>
        /// DbSet for Comments table in mysql database
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// DbSet for Tasks table in mysql database
        /// </summary>
        public DbSet<Task> Tasks { get; set; }

        /// <summary>
        /// Database connection string
        /// </summary>
        public static string ConnectionString { get { return connectionString; } set { connectionString = value; } }

        /// <summary>
        /// constructor for <c>TaskManagerContext</c>
        /// </summary>
        /// <param name="connectionString">configure connection string for mysql database</param>

        public TaskManagerContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(ConnectionString);
        }

    }
}