using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Entities
{
    internal class Status
    {
        /// <summary>
        /// this property represents column Id in mysql database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// this property represents column Name in mysql database
        /// </summary>
        public string Name { get; set; }

    }
}
