using EquipmentRentalHouse.Windows.Catalogue;
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
    public partial class Catalogues : UserControl
    {
        public Catalogues()
        {
            InitializeComponent();
        }

        private void btnDevices_Click(object sender, RoutedEventArgs e)
        {
            CatalogueWindow window = new CatalogueWindow(Catalogue.Device);
            window.ShowDialog();
        }

        private void btnPositions_Click(object sender, RoutedEventArgs e)
        {
            CatalogueWindow window = new CatalogueWindow(Catalogue.Position);
            window.ShowDialog();
        }

        private void btnManufacturers_Click(object sender, RoutedEventArgs e)
        {
            CatalogueWindow window = new CatalogueWindow(Catalogue.Manufacturer);
            window.ShowDialog();
        }

        private void btnStreets_Click(object sender, RoutedEventArgs e)
        {
            CatalogueWindow window = new CatalogueWindow(Catalogue.Street);
            window.ShowDialog();
        }
    }
}
