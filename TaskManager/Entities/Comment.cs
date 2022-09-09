using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Entities
{
    internal class Comment
    {
        /// <summary>
        /// this property represents column Id in mysql database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// this property represents column TaskId in mysql database which is also foreign key reference to table Tasks.Id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// property returns task name from table Tasks based on TaskId
        /// </summary>
        public string Task { get => GetTask(); }

        /// <summary>
        /// this property represents column Contents in mysql database
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// this property represents column DateAdded in mysql database
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// this property represents column UserId in mysql database which is also foreign key reference to table Users.Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// property returns user name from table Users based on UserId
        /// </summary>
        public string User { get => GetUser(); }

        private string GetUser()
        {
            using TaskManagerContext db = new(TaskManagerContext.connectionString);
            var user = (from u in db.Users where u.Id == this.UserId select u.Name).FirstOrDefault();
            return user;
        }

        private string GetTask()
        {
            using TaskManagerContext db = new(TaskManagerContext.connectionString);
            var task = (from t in db.Tasks where t.Id == this.TaskId select t.Name).FirstOrDefault();
            return task;
        }

    }
}
