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
            InitializeButtonStates();
            if (App.Rights.R)
                InitializeCatalogue(cat);
            lblTitle.Content = $"Equipment Rental House - {cat}";
        }

        void InitializeButtonStates()
        {
            if (App.Rights.C == false)
                btnAdd.IsEnabled = false;
            if (App.Rights.R == false)
                btnUpdate.IsEnabled = false;
            if (App.Rights.U == false)
                btnEdit.IsEnabled = false;
            if (App.Rights.D == false)
                btnRemove.IsEnabled = false;
        }


        #region Initialization
        void InitializeCatalogue(Catalogue cat)
        {
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
        }

        void InitializeDevices()
        {
            dgCatalogue.ItemsSource = App.DB.Devices.ToList().OrderBy(x => x.Name);
        }

        void InitializePositions()
        {
            dgCatalogue.ItemsSource = App.DB.Positions.ToList().OrderBy(x => x.Name);
        }

        void InitializeManufacturers()
        {
            dgCatalogue.ItemsSource = App.DB.Manufacturers.ToList().OrderBy(x => x.Name);
        }

        void InitializeStreets()
        {
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
            if (App.Rights.C)
            {
                CatalogueAddEditWindow w = new CatalogueAddEditWindow(_catalogue);
                w.ShowDialog();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (App.Rights.U)
            {
                var obj = dgCatalogue.SelectedItem;
                if (obj != null)
                {
                    CatalogueAddEditWindow w = new CatalogueAddEditWindow(_catalogue, obj);
                    w.ShowDialog();
                }
            }
        }
        #endregion


        #region Remove/Update buttons.
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (App.Rights.D)
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
            if (App.Rights.R)
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