using EquipmentRentalHouse.Windows.Misc;
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

namespace EquipmentRentalHouse.UserControls
{
    public partial class Miscellaneous : UserControl
    {
        public Miscellaneous()
        {
            InitializeComponent();

            if (App.Rights.U == false)
                btn_ChangePassword.IsEnabled = false;
        }

        private void btn_ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow window = new ChangePasswordWindow();
            window.ShowDialog();
        }
    }
}