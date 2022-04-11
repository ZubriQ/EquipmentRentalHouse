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
    public partial class ClientsAddEditWindow : Window
    {
        Client _client;
        List<StockKeepingUnit> _allStock;
        List<StockKeepingUnit> Basket = new List<StockKeepingUnit>(); // Items that'll be not In Stock
        List<StockKeepingUnit> Stock = new List<StockKeepingUnit>(); // Items that still In Stock
        List<Order> Orders = new List<Order>();
        bool _isAdding;
        decimal _totalPrice;

        // Adding.
        public ClientsAddEditWindow()
        {
            InitializeComponent();
            InitializeInitialData();
            InitializeAdding();
            InitializeDataGrids();
        }

        void InitializeAdding()
        {
            _isAdding = true;
            _allStock = App.DB.StockKeepingUnits.Where(s => s.IsInStock == true).ToList();
            InitializeClient();
            DataContext = _client;
            InitializeContents("Equipment Rental House - Ordering", "Add");
        }

        void InitializeClient()
        {
            _client = new Client();
            _client.DateOfOrder = DateTime.Now;
            _client.DateOfBirth = DateTime.Now;
            _client.PassportDateOfIssue = DateTime.Now;
            _client.PassportDateOfExpiration = DateTime.Now;
        }

        // Editing.
        public ClientsAddEditWindow(Client client)
        {
            InitializeComponent();
            InitializeInitialData();
            InitializeEditing(client);
            InitializeDataGrids();
            InitializeOrderPrice();
        }

        void InitializeEditing(Client client)
        {
            _isAdding = false;
            _allStock = App.DB.StockKeepingUnits.Where(s => s.IsInStock == true).ToList();
            _client = client;
            DataContext = _client;
            InitializeContents("Equipment Rental House - Order editing", "Save");
        }

        void InitializeContents(string titleText, string buttonText)
        {
            lblTitle.Content = titleText;
            btnConfirm.Content = buttonText;
        }

        void InitializeOrderPrice()
        {
            foreach (var order in _client.Orders)
                _totalPrice += order.StockKeepingUnit.RentalPrice;
            lblTotalPrice.Content = _totalPrice.ToString();
        }

        void InitializeInitialData()
        {
            cbStreet.ItemsSource = App.DB.Streets.ToArray();
        }

        void InitializeDataGrids()
        {
            ucAvailableDevices.DataGrid.Columns[2].Visibility = Visibility.Hidden;
            dgBasket.Columns[2].Visibility = Visibility.Hidden;
            Orders = _client.Orders.ToList();
            dgBasket.ItemsSource = Orders;
            Stock.AddRange(_allStock);
            ucAvailableDevices.DataGrid.ItemsSource = Stock;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchbarPlaceholderState();

            if (txtSearch.Text.Length > 1)
                SearchingItems(); // Show filtered list.
            else NotSearchingItems(); // Show all items in the stock.
        }

        void SearchbarPlaceholderState()
        {
            if (txtSearch.Text == "")
                tbSearchPlaceholder.Text = "search by number";
            else tbSearchPlaceholder.Text = "";
        }

        void SearchingItems() // TODO: filters
        {
            Stock = Stock.Where(s => s.Number.Contains(txtSearch.Text))
                         .Except(Basket)
                         .ToList();
            ucAvailableDevices.DataGrid.ItemsSource = Stock;
        }

        void NotSearchingItems()
        {
            Stock = _allStock.Where(s => s.Number.Contains(txtSearch.Text) &&
                                         s.IsInStock == true)
                             .Except(Basket)
                             .ToList();
            ucAvailableDevices.DataGrid.ItemsSource = Stock;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (_isAdding)
            {
                if (Basket.Count > 0) // TODO: check the client's fields with Regex.
                {
                    try
                    {
                        App.DB.Clients.Add(_client);
                        App.DB.SaveChanges();
                    }
                    catch { }
                }
            }
            else // Then it's editing.
            {
                try
                {
                    App.DB.Orders.AddRange(Orders);
                    App.DB.SaveChanges();
                }
                catch { }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void brdBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnAddToBasket_Click(object sender, RoutedEventArgs e)
        {
            TransferToBasket();
        }
        private void btnRemoveFromBasket_Click(object sender, RoutedEventArgs e)
        {
            TransferToStock();
        }

        void TransferToBasket()
        {
            var item = ucAvailableDevices.DataGrid.SelectedItem as StockKeepingUnit;
            if (item != null)
            {
                var buyingDevice = AddItem(item); // Move item to the left.
                RefreshTables(); // Update.
                TotalPriceChange(buyingDevice.StockKeepingUnit.RentalPrice);
            }
        }

        Order AddItem(StockKeepingUnit skuItem)
        {
            skuItem.IsInStock = false;
            Basket.Add(skuItem);
            Stock.Remove(skuItem);
            var buyingDevice = new Order()
            {
                Client = _client,
                StockKeepingUnit = skuItem,
                DateOfExpiration = DateTime.Now.AddDays(7),
                DateOfOrder = DateTime.Now,
                IsReturned = false,
                ClientId = _client.Id,
                StockKeepingUnitId = skuItem.Id,
            };
            Orders.Add(buyingDevice);
            return buyingDevice;
        }

        void TransferToStock()
        {
            var orderingItem = dgBasket.SelectedItem as Order;
            if (orderingItem != null)
            {
                RemoveItem(orderingItem); // Move item to the right.
                RefreshTables(); // Update DataGrid.
                TotalPriceChange(-orderingItem.StockKeepingUnit.RentalPrice);
            }
        }

        void RemoveItem(Order orderItem)
        {
            var skuItem = orderItem.StockKeepingUnit;
            skuItem.IsInStock = true;
            Stock.Add(skuItem);
            Basket.Remove(skuItem);
            Orders.Remove(orderItem);
        }

        void RefreshTables()
        {
            dgBasket.Items.Refresh();
            ucAvailableDevices.DataGrid.Items.Refresh();
            ucAvailableDevices.DataGrid.SelectedIndex = -1;
            dgBasket.SelectedIndex = -1;
        }

        void TotalPriceChange(decimal value)
        {
            _totalPrice += value;
            lblTotalPrice.Content = _totalPrice.ToString();
        }
    }
}