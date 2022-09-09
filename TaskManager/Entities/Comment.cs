using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Entities
{
    internal class Comment
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Contents { get; set; }
        public DateTime DateAdded { get; set; }
        public int UserId { get; set; }
        public string User { get => GetUser(); }

        private string GetUser()
        {
            using TaskManagerContext db = new(TaskManagerContext.connectionString);
            var user = (from u in db.Users where u.Id == this.UserId select u.Name).FirstOrDefault();
            return user;
        }

    }
}
