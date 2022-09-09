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

namespace TaskManager.Windows
{
    /// <summary>
    /// Interaction logic for UpdateTaskWindow.xaml
    /// </summary>
    public partial class UpdateTaskWindow : Window
    {

        /// <summary>
        /// Constructor for <c>UpdateTaskWindow</c>
        /// </summary>
        public UpdateTaskWindow()
        {
            InitializeComponent();
            cmbStatus.ItemsSource = GetStatuses();
        }

        private static List<string> GetStatuses()
        {
            List<string> statuses = new();
            using (TaskManagerContext db = new(TaskManagerContext.connectionString))
            {
                statuses = (from status in db.Statuses select status.Name).ToList();
            }
            return statuses;
        }

        private void AddComment_Click(object sender, RoutedEventArgs e)
        {
            AddCommentWindow window = new();
            window.tbxId.Text = tbxId.Text;
            window.tbxTaskName.Text = tbxTaskName.Text;
            window.ShowDialog();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbxTaskName.Text))
            {
                try
                {
                    using TaskManagerContext db = new(TaskManagerContext.connectionString);
                    var task = (from t in db.Tasks where t.Id == Int32.Parse(tbxId.Text) select t).FirstOrDefault();
                    task.Name = tbxTaskName.Text;
                    task.StatusId = cmbStatus.SelectedIndex;
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
