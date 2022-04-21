using EquipmentRentalHouse.Database;
using EquipmentRentalHouse.Windows.Clients;
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
    public partial class Clients : UserControl
    {
        List<Client> _clients;

        public Clients()
        {
            InitializeComponent();
            HidePassportData();
            _clients = App.DB.Clients.ToList();
            dgClients.ItemsSource = _clients;
        }

        private void chkShowPassportData_Checked(object sender, RoutedEventArgs e)
        {
            ShowPassportData();
        }
        void ShowPassportData()
        {
            dgClients.Columns[10].Width = new DataGridLength(1, DataGridLengthUnitType.Auto);
            dgClients.Columns[11].Visibility = Visibility.Visible;
            dgClients.Columns[12].Visibility = Visibility.Visible;
            dgClients.Columns[13].Visibility = Visibility.Visible;
            dgClients.Columns[14].Visibility = Visibility.Visible;
            dgClients.Columns[15].Visibility = Visibility.Visible;
            dgClients.Columns[16].Visibility = Visibility.Visible;
        }
        private void chkShowPassportData_Unchecked(object sender, RoutedEventArgs e)
        {
            HidePassportData();
        }
        void HidePassportData()
        {
            dgClients.Columns[10].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgClients.Columns[11].Visibility = Visibility.Hidden;
            dgClients.Columns[12].Visibility = Visibility.Hidden;
            dgClients.Columns[13].Visibility = Visibility.Hidden;
            dgClients.Columns[14].Visibility = Visibility.Hidden;
            dgClients.Columns[15].Visibility = Visibility.Hidden;
            dgClients.Columns[16].Visibility = Visibility.Hidden;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchbarPlaceholderState();

            if (txtSearch.Text.Length > 1)
                SearchEmployee();
            else
                dgClients.ItemsSource = _clients.ToList();
        }
        void SearchbarPlaceholderState()
        {
            if (txtSearch.Text == "")
                tbSearchPlaceholder.Text = "search by full name";
            else tbSearchPlaceholder.Text = "";
        }
        void SearchEmployee()
        {
            var search = txtSearch.Text.ToLower().Trim();
            List<Client> clients = _clients.ToList();
            try
            {
                dgClients.ItemsSource = clients.Where(s => s.FullName.ToLower().Contains(search));
                //dgClients.ItemsSource = clients.Where(s => s.FirstName.ToLower().Contains(search) ||
                //                                           s.Surname.ToLower().Contains(search) ||
                //                                           s.Patronymic.ToLower().Contains(search));
            }
            catch { }
        }

        private void btnDoubleLeftArrow_Click(object sender, RoutedEventArgs e)
        {
            dgClients.SelectedIndex = 0;
        }
        private void btnLeftArrow_Click(object sender, RoutedEventArgs e)
        {
            if ((dgClients.SelectedItem != null) && (dgClients.SelectedIndex > 0))
                dgClients.SelectedIndex -= 1;
        }
        private void btnRightArrow_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem != null)
                dgClients.SelectedIndex += 1;
        }
        private void btnDoubleRightArrow_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.Items.Count > 0)
                dgClients.SelectedIndex = dgClients.Items.Count - 1;
        }
        private void btnShowPassport_Click(object sender, RoutedEventArgs e)
        {
            var client = dgClients.SelectedItem;
            if (client is Client)
            {
                ClientPassportWindow w = new ClientPassportWindow(client as Client);
                w.ShowDialog();
            }
        }
        private void btnShowOrderedItems_Click(object sender, RoutedEventArgs e)
        {
            var client = dgClients.SelectedItem;
            if (client is Client)
            {
                if ((client as Client).Orders.Count > 0)
                {
                    RentedDevicesWindow w = new RentedDevicesWindow(client as Client);
                    w.ShowDialog();
                }
                else MessageBox.Show($"The selected client has no items.");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ClientAddEditWindow window = new ClientAddEditWindow();
            window.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem != null)
            {
                var client = dgClients.SelectedItem as Client;
                ClientAddEditWindow window = new ClientAddEditWindow(client);
                window.ShowDialog();
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var obj = dgClients.SelectedItem as Client;
            if (obj != null)
            {
                if (MessageBox.Show($"Remove the selected client?", "Removing",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    try
                    {
                        App.DB.Clients.Remove(obj);
                        App.DB.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show($"Error: the unit hasn't been removed.");
                    }
                    MessageBox.Show($"The client has successfully been removed.");
                    Update();
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        void Update()
        {
            _clients = App.DB.Clients.ToList();
            dgClients.Items.Refresh();
            //dgClients.ItemsSource = _clients;
        }
    }
}
