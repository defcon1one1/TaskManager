using Microsoft.Data.SqlClient;
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

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection loginConnection = new SqlConnection(TaskManagerContext.connectionString);
            try
            {
                if (loginConnection.State == System.Data.ConnectionState.Closed)
                    loginConnection.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE Name=@Name AND Password=@Password";

                SqlCommand sqlCmd = new SqlCommand(query, loginConnection);

                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Name", tbxUser.Text);
                sqlCmd.Parameters.AddWithValue("@Password", pbxPassword.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                if (count == 1)
                {
                    MainWindow.user = tbxUser.Text;
                    MainWindow dashboard = new MainWindow();

                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("User or password is not correct!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
