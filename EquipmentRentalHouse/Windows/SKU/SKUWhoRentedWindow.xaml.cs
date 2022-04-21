using EquipmentRentalHouse.Database;
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

namespace EquipmentRentalHouse.Windows.SKU
{
    public partial class SKUWhoRentedWindow : Window
    {
        Order _order;
        public SKUWhoRentedWindow(Order order)
        {
            InitializeComponent();
            _order = order;
            DataContext = _order;
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
    }
}
