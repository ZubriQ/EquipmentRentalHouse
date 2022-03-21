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

namespace EquipmentRentalHouse
{
    public partial class LoginWindow : Window
    {
        public string Username
        {
            get
            {
                return txtUsername.Text.Trim();
            }
        }
        public string Password
        {
            get
            {
                return pbPassword.Password.Trim();
            }
        }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void brdBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtUsername.Text == "")
                tbUsernamePlaceholder.Text = "username";
            else tbUsernamePlaceholder.Text = "";
        }

        private void pbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbPassword.Password == "")
                tbPasswordPlaceholder.Text = "password";
            else tbPasswordPlaceholder.Text = "";
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }
    }
}
