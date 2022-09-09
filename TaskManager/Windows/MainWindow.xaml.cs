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
        private static string user = "unknown";

        public MainWindow()
        {

            InitializeComponent();
            LoggedUser.Text = User;
            dgridTasks.ItemsSource = GetTasks();
        }

        public static string User { get => user; set => user = value; }

        private static List<Task> GetTasks()
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
            AddTaskWindow window = new();
            window.tbxAuthor.Text = User;
            window.ShowDialog();
            dgridTasks.ItemsSource = GetTasks();
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (dgridTasks.SelectedItem != null)
            {
                try
                {
                    using TaskManagerContext db = new(TaskManagerContext.connectionString);
                    Task task = (Task)dgridTasks.SelectedItem;
                    db.Remove(task);
                    db.SaveChanges();
                    dgridTasks.ItemsSource = GetTasks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void UpdateTask_Click(object sender, RoutedEventArgs e)
        {
            if (dgridTasks.SelectedItem != null)
            {
                try
                {
                    using TaskManagerContext db = new(TaskManagerContext.connectionString);
                    Task task = (Task)dgridTasks.SelectedItem;
                    UpdateTaskWindow window = new();
                    window.tbxId.Text = task.Id.ToString();
                    window.tbxTaskName.Text = task.Name;
                    window.cmbStatus.SelectedIndex = task.StatusId;
                    window.tbxAuthor.Text = (from u in db.Users where u.Id == task.UserId select u.Name).FirstOrDefault();
                    window.tbxCreated.Text = task.DateCreated.ToString("dd-MM-yyyy");
                    window.ShowDialog();
                    dgridTasks.ItemsSource = GetTasks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
