using EquipmentRentalHouse.Database;
using EquipmentRentalHouse.Windows.SKU;
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
    public partial class SKU : UserControl
    {
        List<StockKeepingUnit> _SKUs = new List<StockKeepingUnit>();

        public SKU()
        {
            InitializeComponent();
            _SKUs = App.DB.StockKeepingUnits.ToList();
            dgSKUs.ItemsSource = _SKUs.Where(s => s.IsInStock == true).ToList();
        }

        private void chkIsInStock_Unchecked(object sender, RoutedEventArgs e)
        {
            dgSKUs.ItemsSource = _SKUs.Where(s => s.IsInStock == false).ToList();
        }

        private void chkIsInStock_Checked(object sender, RoutedEventArgs e)
        {
            if (dgSKUs != null)
                dgSKUs.ItemsSource = _SKUs.Where(s => s.IsInStock == true).ToList();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchbarPlaceholderState();

            if (txtSearch.Text.Length > 1)
                SearchSKU();
            else
                SKUNotFound();
        }

        void SearchbarPlaceholderState()
        {
            if (txtSearch.Text == "")
                tbSearchPlaceholder.Text = "search by number";
            else tbSearchPlaceholder.Text = "";
        }

        void SearchSKU()
        {
            var search = txtSearch.Text.ToLower().Trim();
            List<StockKeepingUnit> sku;
            if (chkIsInStock.IsChecked == true)
                sku = _SKUs.Where(s => s.IsInStock == true).ToList();
            else sku = _SKUs.Where(s => s.IsInStock == false).ToList();

            dgSKUs.ItemsSource = sku.Where(s => s.Number.ToLower().Contains(search));
        }

        void SKUNotFound()
        {
            if (chkIsInStock.IsChecked == false)
                dgSKUs.ItemsSource = _SKUs.Where(s => s.IsInStock == false).ToArray();
            else dgSKUs.ItemsSource = _SKUs.Where(s => s.IsInStock == true).ToArray();
        }

        private void btnDoubleLeftArrow_Click(object sender, RoutedEventArgs e)
        {
            dgSKUs.SelectedIndex = 0;
        }
        private void btnLeftArrow_Click(object sender, RoutedEventArgs e)
        {
            if ((dgSKUs.SelectedItem != null) && (dgSKUs.SelectedIndex > 0))
                dgSKUs.SelectedIndex -= 1;
        }
        private void btnRightArrow_Click(object sender, RoutedEventArgs e)
        {
            if (dgSKUs.SelectedItem != null)
                dgSKUs.SelectedIndex += 1;
        }
        private void btnDoubleRightArrow_Click(object sender, RoutedEventArgs e)
        {
            if (dgSKUs.Items.Count > 0)
                dgSKUs.SelectedIndex = dgSKUs.Items.Count - 1;
        }

        private void btnShowWhoRented_Click(object sender, RoutedEventArgs e)
        {
            var item = dgSKUs.SelectedItem as StockKeepingUnit;
            if (item != null)
            {
                var order = item.Orders.Where(o => o.IsReturned == false).FirstOrDefault();
                if (order != null)
                {
                    var window = new SKUWhoRentedWindow(order);
                    window.ShowDialog();
                }
                else MessageBox.Show("The selected item is in stock.");
            }
        }

        void UpdateDataGrid()
        {
            if (chkIsInStock.IsChecked == true)
                dgSKUs.ItemsSource = App.DB.StockKeepingUnits.Where(s => s.IsInStock == true).ToArray();
            else dgSKUs.ItemsSource = App.DB.StockKeepingUnits.Where(s => s.IsInStock == false).ToArray();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SKUAddEditWindow window = new SKUAddEditWindow();
            window.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgSKUs.SelectedItem != null)
            {
                var sku = dgSKUs.SelectedItem as StockKeepingUnit;
                SKUAddEditWindow window = new SKUAddEditWindow(sku);
                window.ShowDialog();
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var obj = dgSKUs.SelectedItem as StockKeepingUnit;
            if (obj != null)
            {
                if (MessageBox.Show($"Remove the selected unit?", "Removing",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    try
                    {
                        App.DB.StockKeepingUnits.Remove(obj);
                        App.DB.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show($"Error: the unit hasn't been removed.");
                    }
                    MessageBox.Show($"The unit has successfully been removed.");
                    UpdateDataGrid();
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid(); // Or load from App.DB.
        }
    }
}
