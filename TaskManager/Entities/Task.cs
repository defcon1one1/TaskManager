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
        public int Priority { get; set; }
        public DateTime Created { get; set; }
        public int StatusId { get; set; }
        public string Status { get => GetStatus(); }

        private string GetStatus()
        {
            using (TaskManagerContext db = new TaskManagerContext(TaskManagerContext.connectionString))
            {
                var status = (from s in db.Statuses where Id == this.StatusId select s.Name).FirstOrDefault();
                return status;
            }
        }
    }
}
