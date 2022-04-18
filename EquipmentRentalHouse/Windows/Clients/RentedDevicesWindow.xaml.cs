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

namespace EquipmentRentalHouse.Windows.Clients
{
    public partial class RentedDevicesWindow : Window
    {
        Client _client;
        List<Order> _orders;
        //List<StockKeepingUnit> _skus;

        public RentedDevicesWindow(Client client)
        {
            InitializeComponent();
            _client = client;
            DataContext = _client;

            _orders = _client.Orders.ToList();
            dgSKUs.ItemsSource = _orders;

            //_skus = App.DB.StockKeepingUnits.Where(order => _orders.Any(s => s.StockKeepingUnitId == order.Id)).ToList();
            //int k = 0;
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_ReturnToStock_Click(object sender, RoutedEventArgs e)
        {
            var item = dgSKUs.SelectedItem as Order;
            if (item != null)
            {
                if (MessageBox.Show($"Are you sure you want to return the selected item to the stock?", "Returning",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    item.IsReturned = true;
                    var sku = App.DB.StockKeepingUnits.Where(s => s.Id == item.StockKeepingUnitId).FirstOrDefault();
                    sku.IsInStock = true;
                    UpdateDataGrid();
                }
            }
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.DB.SaveChanges();
                MessageBox.Show($"Successfully updated.");
                Close();
            }
            catch
            {
                MessageBox.Show($"Something went wrong.");
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void UpdateDataGrid()
        {
            _orders = _orders.Where(i => i.IsReturned == false).ToList();
            dgSKUs.Items.Refresh();
        }

        private void brd_Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}