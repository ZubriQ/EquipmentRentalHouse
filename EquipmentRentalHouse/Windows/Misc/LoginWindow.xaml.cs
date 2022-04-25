using EquipmentRentalHouse.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace EquipmentRentalHouse.Windows.Misc
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
                return pbPassword.Password;
            }
        }

        public LoginWindow()
        {
            InitializeComponent();
            txtUsername.Focus();
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
            if (UserDataChecker.CheckLogin(Username))
                SignIn();
            else
                MessageBox.Show("Username can contain only letters, digits or underscores.");
        }

        void SignIn()
        {
            var user = App.DB.Users.FirstOrDefault(u => u.Login == txtUsername.Text);
            var valid = user?.ObjectsUsers.FirstOrDefault(v => v.Object.Name == "Equipment Rental House");
            // Check if the user has rights.
            if (valid != null)
            {
                bool access = Validate(user);
                if (access)
                {
                    App.User = user;
                    App.Rights = valid;
                    MainWindow window = new MainWindow();
                    window.Show();
                    Close();
                }
                else MessageBox.Show("Incorrect user password.");
            }
            else MessageBox.Show("Incorrect user login.");
        }

        bool Validate(User user)
        {
            string savedPasswordHash = user.Password.ToString();
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(pbPassword.Password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            bool access = true;
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    access = false;
            return access;
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter) && (!string.IsNullOrWhiteSpace(txtUsername.Text)))
            {
                pbPassword.Focus();
            }
        }

        private void pbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter) && string.IsNullOrWhiteSpace(pbPassword.Password))
                MessageBox.Show("Please, enter the password.");
            else if (e.Key == Key.Enter)
                btnSignIn_Click(sender, e);
        }
    }
}