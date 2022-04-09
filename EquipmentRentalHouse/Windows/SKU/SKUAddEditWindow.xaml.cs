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
    public partial class SKUAddEditWindow : Window
    {
        StockKeepingUnit _sku;
        bool _isAdding;

        // Adding.
        public SKUAddEditWindow()
        {
            InitializeComponent();
            InitializeComboboxes();
            InitializeAdding();
        }

        // Editing.
        public SKUAddEditWindow(StockKeepingUnit sku)
        {
            InitializeComponent();
            InitializeComboboxes();
            InitializeEditing(sku);
        }

        void InitializeEditing(StockKeepingUnit sku)
        {
            lblTitle.Content = "Equipment Rental House - Editing an existing SKU";
            btnConfirm.Content = "Save";
            _isAdding = false;

            _sku = sku;
            DataContext = _sku;
        }

        void InitializeAdding()
        {
            lblTitle.Content = "Equipment Rental House - Adding a new SKU";
            btnConfirm.Content = "Add";
            _isAdding = true;

            _sku = new StockKeepingUnit();
            _sku.Number = Guid.NewGuid().ToString();
            _sku.IsInStock = true;
            DataContext = _sku;
        }

        void InitializeComboboxes()
        {
            cbDevice.ItemsSource = App.DB.Devices.ToArray();
            cbManufacturer.ItemsSource = App.DB.Manufacturers.ToArray();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void brdBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        // Add/Edit.
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (_isAdding)
            {
                if (FinishAdding())
                    Close();
            }
            else
            {
                FinishEditing();
                Close();
            }
        }

        bool FinishAdding()
        {
            try
            {
                App.DB.StockKeepingUnits.Add(_sku);
                App.DB.SaveChanges();
                MessageBox.Show($"The SKU has been successfully added.");
                return true;
            }
            catch
            {
                MessageBox.Show($"Error: The fields are filled incorrectly.");
                return false;
            }
        }

        void FinishEditing()
        {
            try
            {
                App.DB.SaveChanges();
                MessageBox.Show($"The SKU has been successfully changed.");
            }
            catch { }
        }
    }
}
