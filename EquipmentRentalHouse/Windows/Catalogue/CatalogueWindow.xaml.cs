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

namespace EquipmentRentalHouse.Windows.Catalogue
{
    public partial class CatalogueWindow : Window
    {
        Catalogue _catalogue;

        public CatalogueWindow(Catalogue cat) // :3
        {
            InitializeComponent();
            _catalogue = cat;
            switch (cat)
            {
                case Catalogue.Device:
                    InitializeDevices();
                    break;
                case Catalogue.Position:
                    InitializePositions();
                    break;
                case Catalogue.Manufacturer:
                    InitializeManufacturers();
                    break;
                case Catalogue.Street:
                    InitializeStreets();
                    break;
            }
            lblTitle.Content = $"Equipment Rental House - {cat}";
        }


        #region Initialization
        void InitializeDevices()
        {
            lblTitle.Content = "Equipment Rental House - Devices";
            dgCatalogue.ItemsSource = App.DB.Devices.ToList().OrderBy(x => x.Name);
        }

        void InitializePositions()
        {
            lblTitle.Content = "Equipment Rental House - Positions";
            dgCatalogue.ItemsSource = App.DB.Positions.ToList().OrderBy(x => x.Name);
        }

        void InitializeManufacturers()
        {
            lblTitle.Content = "Equipment Rental House - Manufacturers";
            dgCatalogue.ItemsSource = App.DB.Manufacturers.ToList().OrderBy(x => x.Name);
        }

        void InitializeStreets()
        {
            lblTitle.Content = "Equipment Rental House - Streets";
            dgCatalogue.ItemsSource = App.DB.Streets.ToList().OrderBy(x => x.Name);
        }
        #endregion


        private void brdBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        #region Add/Edit buttons.
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            CatalogueAddEditWindow w = new CatalogueAddEditWindow(_catalogue);
            w.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var obj = dgCatalogue.SelectedItem;
            if (obj != null)
            {
                CatalogueAddEditWindow w = new CatalogueAddEditWindow(_catalogue, obj);
                w.ShowDialog();
            }
        }
        #endregion


        #region Remove/Update buttons.
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var obj = dgCatalogue.SelectedItem;
            if (obj != null)
            {
                if (MessageBox.Show($"Remove the selected {_catalogue}?", "Removing",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    RemoveObject(obj);
                    App.DB.SaveChanges();
                    MessageBox.Show($"The {_catalogue} has successfully been removed.");
                    UpdateDataGrid();
                }
            }
        }

        void RemoveObject(object obj)
        {
            switch (_catalogue)
            {
                case Catalogue.Device:
                    App.DB.Devices.Remove(obj as Device);
                    break;
                case Catalogue.Manufacturer:
                    App.DB.Manufacturers.Remove(obj as Manufacturer);
                    break;
                case Catalogue.Position:
                    App.DB.Positions.Remove(obj as Position);
                    break;
                case Catalogue.Street:
                    App.DB.Streets.Remove(obj as Street);
                    break;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        public void UpdateDataGrid()
        {
            switch (_catalogue)
            {
                case Catalogue.Device:
                    dgCatalogue.ItemsSource = App.DB.Devices.ToArray().OrderBy(x => x.Name);
                    break;
                case Catalogue.Manufacturer:
                    dgCatalogue.ItemsSource = App.DB.Manufacturers.ToArray().OrderBy(x => x.Name);
                    break;
                case Catalogue.Position:
                    dgCatalogue.ItemsSource = App.DB.Positions.ToArray().OrderBy(x => x.Name);
                    break;
                case Catalogue.Street:
                    dgCatalogue.ItemsSource = App.DB.Streets.ToArray().OrderBy(x => x.Name);
                    break;
            }
        }
        #endregion
    }
}