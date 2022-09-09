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
    /// Interaction logic for ViewCommentsWindow.xaml
    /// </summary>
    public partial class ViewCommentsWindow : Window
    {

        public static int taskId;

        public ViewCommentsWindow()
        {
            InitializeComponent();
            dgridComments.ItemsSource = GetComments();
        }

        private static List<Comment> GetComments()
        {
            List<Comment> comments = new();
            using (TaskManagerContext db = new(TaskManagerContext.connectionString))
            {
                comments = (from comment in db.Comments where comment.TaskId == taskId select comment).ToList();
            }
            return comments;
        }

        private void DeleteComment_Click(object sender, RoutedEventArgs e)
        {
            if (dgridComments.SelectedItem != null)
            {
                try
                {
                    using TaskManagerContext db = new(TaskManagerContext.connectionString);
                    Comment comment = (Comment)dgridComments.SelectedItem;
                    db.Remove(comment);
                    db.SaveChanges();
                    dgridComments.ItemsSource = GetComments();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
