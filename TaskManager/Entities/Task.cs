using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Entities
{
    internal class Task
    {
        /// <summary>
        /// this property represents column Id in mysql database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// this property represents column Name in mysql database
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// this property represents column DateCreated in mysql database
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// this property represents column StatusId in mysql database which also is foreign key reference to table Status
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// this property represents column UserId in mysql database which also is foreign key reference to table Users
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// this property returns Status name from table Statuses based on StatusId 
        /// </summary>
        public string Status { get => GetStatus(); }

        /// <summary>
        /// this property returns User name who added task, from table Users based on UserId 
        /// </summary>
        public string AddedBy { get => GetUser(); }

        private string GetStatus()
        {
            using TaskManagerContext db = new(TaskManagerContext.connectionString);
            var status = (from s in db.Statuses where s.Id == this.StatusId select s.Name).FirstOrDefault();
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
