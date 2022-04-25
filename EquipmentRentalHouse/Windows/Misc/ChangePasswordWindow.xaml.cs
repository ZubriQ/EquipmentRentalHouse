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
    public partial class ChangePasswordWindow : Window
    {
        User _user;
        public ChangePasswordWindow()
        {
            InitializeComponent();
            _user = App.User;
            DataContext = _user;
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void brd_Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void pb_CurrentPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pb_CurrentPassword.Password == "")
                tb_CurrentPasswordPlaceholder.Text = "current password";
            else tb_CurrentPasswordPlaceholder.Text = "";
        }
        private void pb_NewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pb_NewPassword.Password == "")
                tb_NewPasswordPlaceholder.Text = "new password";
            else tb_NewPasswordPlaceholder.Text = "";
        }
        private void pb_ConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pb_ConfirmPassword.Password == "")
                tb_ConfirmPasswordPlaceholder.Text = "confirm password";
            else tb_ConfirmPasswordPlaceholder.Text = "";
        }

        private void btn_Change_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataChecker.CheckPassword(pb_NewPassword.Password))
            {
                byte[] hashBytes = Convert.FromBase64String(_user.Password);
                byte[] hash = GetHash(hashBytes);
                bool access = CheckAccess(hashBytes, hash);

                if (access && (pb_NewPassword.Password == pb_ConfirmPassword.Password))
                    ChangePassword();
                else MessageBox.Show("Passwords do not match.");
            }
            else MessageBox.Show("New password can contain letters, digits or " +
                "special symbols. Password length must be at least 6 characters.");
        }

        byte[] GetHash(byte[] hashBytes)
        {
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(pb_CurrentPassword.Password, salt, 10000);
            return pbkdf2.GetBytes(20);
        }

        bool CheckAccess(byte[] hashBytes, byte[] hash)
        {
            bool access = true;
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    access = false;
            return access;
        }

        void ChangePassword()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(pb_NewPassword.Password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            _user.Password = savedPasswordHash;
            App.DB.SaveChanges();
            MessageBox.Show("Password has been changed successfully.");
        }
    }
}
