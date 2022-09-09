using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskManager.Entities;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Windows
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        /// <summary>
        /// constructor for <c>AddTaskWindow</c>
        /// </summary>
        public AddTaskWindow()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using TaskManagerContext db = new(TaskManagerContext.connectionString);
            if (!String.IsNullOrWhiteSpace(tbxTaskName.Text))
            {
                try
                {
                    Task task = new()
                    {
                        Name = tbxTaskName.Text,
                        StatusId = 0,
                        DateCreated = DateTime.Today,
                        UserId = (from u in db.Users where u.Name == tbxAuthor.Text select u.Id).FirstOrDefault()
                    };
                    db.Add(task);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid input");
                }
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
