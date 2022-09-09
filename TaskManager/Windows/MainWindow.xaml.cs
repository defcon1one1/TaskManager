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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskManager.Entities;
using TaskManager.Windows;
using Task = TaskManager.Entities.Task;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string user = "unknown";

        public MainWindow()
        {

            InitializeComponent();
            LoggedUser.Text = user;
            dgridTasks.ItemsSource = GetTasks();
        }

        private List<Task> GetTasks()
        {
            List<Task> tasks = new();
            using (TaskManagerContext db = new(TaskManagerContext.connectionString))
            {
                tasks = (from task in db.Tasks select task).ToList();
            }
            return tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWindow = new();
            addTaskWindow.tbxAuthor.Text = user;
            addTaskWindow.ShowDialog();
            dgridTasks.ItemsSource = GetTasks();
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (dgridTasks.SelectedItem != null)
            {
                try
                {
                    using (TaskManagerContext db = new(TaskManagerContext.connectionString))
                    {
                        Task task = (Task)dgridTasks.SelectedItem;
                        db.Remove(task);
                        db.SaveChanges();
                        dgridTasks.ItemsSource = GetTasks();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
