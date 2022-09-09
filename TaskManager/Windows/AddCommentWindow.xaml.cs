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
    /// Interaction logic for AddCommentWindow.xaml
    /// </summary>
    public partial class AddCommentWindow : Window
    {
        /// <summary>
        /// Constructor for <c>AddCommentWindow</c>
        /// </summary>
        public AddCommentWindow()
        {
            InitializeComponent();
        }

        private void AddComment_Click(object sender, RoutedEventArgs e)
        {
            using TaskManagerContext db = new(TaskManagerContext.connectionString);
            if (!String.IsNullOrWhiteSpace(tbxContents.Text))
            {
                try
                {
                    Comment comment = new()
                    {
                        Contents = tbxContents.Text,
                        TaskId = Int32.Parse(tbxId.Text),
                        DateAdded = DateTime.Today,
                        UserId = (from u in db.Users where u.Name == MainWindow.User select u.Id).FirstOrDefault()
                    };
                    db.Add(comment);
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
