using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Entities
{
    internal class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public string Status { get => GetStatus(); }
        public string AddedBy { get => GetUser(); }

        private string GetStatus()
        {
            using TaskManagerContext db = new(TaskManagerContext.connectionString);
            var status = (from s in db.Statuses where Id == this.StatusId select s.Name).FirstOrDefault();
            return status;
        }

        private string GetUser()
        {
            using TaskManagerContext db = new(TaskManagerContext.connectionString);
            var user = (from u in db.Users where u.Id == this.UserId select u.Name).FirstOrDefault();
            return user;
        }
    }
}
