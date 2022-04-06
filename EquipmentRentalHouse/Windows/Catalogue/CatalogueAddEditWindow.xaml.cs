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
    public partial class CatalogueAddEditWindow : Window
    {
        Catalogue _catalogue;
        object _object;
        bool _isAdding = false;

        // Adding.
        public CatalogueAddEditWindow(Catalogue cat) 
        {
            InitializeComponent();
            InitializeAdding(cat);
        }

        void InitializeAdding(Catalogue cat)
        {
            _catalogue = cat;
            _isAdding = true;
            lblTitle.Content = $"{cat} - Adding";
            btnConfirm.Content = "Add";
        }

        // Editing.
        public CatalogueAddEditWindow(Catalogue cat, object obj)
        {
            InitializeComponent();
            InitializeEditing(cat, obj);
        }

        void InitializeEditing(Catalogue cat, object obj)
        {
            _catalogue = cat;
            _object = obj;
            DataContext = _object;
            lblTitle.Content = $"{cat} - Editing";
            btnConfirm.Content = "Save";
        }

        #region Add/Edit.
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
                AddObject();
                App.DB.SaveChanges();
                MessageBox.Show($"New {_catalogue} has been successfully added.");
                return true;
            }
            catch
            {
                MessageBox.Show($"Error: The fields are filled incorrectly.");
                return false;
            }
        }

        void AddObject()
        {
            switch (_catalogue)
            {
                case Catalogue.Device:
                    AddDevice();
                    break;
                case Catalogue.Manufacturer:
                    AddManufacturer();
                    break;
                case Catalogue.Position:
                    AddPosition();
                    break;
                case Catalogue.Street:
                    AddStreet();
                    break;
            }
        }

        void AddDevice()
        {
            var device = new Device();
            device.Name = txtCatalogueName.Text;
            App.DB.Devices.Add(device);
        }

        void AddManufacturer()
        {
            var manufacturer = new Manufacturer();
            manufacturer.Name = txtCatalogueName.Text;
            App.DB.Manufacturers.Add(manufacturer);
        }

        void AddPosition()
        {
            var position = new Position();
            position.Name = txtCatalogueName.Text;
            App.DB.Positions.Add(position);
        }

        void AddStreet()
        {
            var street = new Street();
            street.Name = txtCatalogueName.Text;
            App.DB.Streets.Add(street);
        }


        void FinishEditing()
        {
            try
            {
                App.DB.SaveChanges();
                MessageBox.Show($"The {_catalogue} has been successfully changed.");
            }
            catch { }
        }
        #endregion

        #region Misc
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
        #endregion
    }
}
