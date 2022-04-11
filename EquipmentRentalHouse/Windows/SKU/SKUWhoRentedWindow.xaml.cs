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
    /// <summary>
    /// Логика взаимодействия для SKUWhoRentedWindow.xaml
    /// </summary>
    public partial class SKUWhoRentedWindow : Window
    {
        Client _client;
        public SKUWhoRentedWindow(Client client)
        {
            InitializeComponent();
            _client = client;
            DataContext = _client;
        }
    }
}
