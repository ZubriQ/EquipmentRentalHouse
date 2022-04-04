using EquipmentRentalHouse.Database;
using EquipmentRentalHouse.Windows.Employees;
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
    /// <summary>
    /// Логика взаимодействия для Employees.xaml
    /// </summary>
    public partial class Employees : UserControl
    {
        List<Staff> _staff;

        public Employees()
        {
            InitializeComponent();
            _staff = App.DB.Staffs.ToList();
            dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == false).ToArray();
        }


        #region Add, Edit, Dismiss controls
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            StaffWindow window = new StaffWindow();
            window.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployees.SelectedItem != null)
            {
                var employee = dgEmployees.SelectedItem as Staff;
                StaffWindow window = new StaffWindow(employee);
                window.ShowDialog();
            }
        }

        private void btnDismiss_Click(object sender, RoutedEventArgs e)
        {
            var employee = dgEmployees.SelectedItem as Staff;
            if ((employee != null) && (employee.IsDismissed == false))
            {
                DismissEmployee(employee);
                UpdateDataGrid();
            }
            else if ((employee != null) && (employee.IsDismissed == true))
            {
                ReturnDismissedEmployee(employee);
                UpdateDataGrid();
            }
        }

        void DismissEmployee(Staff employee)
        {
            if (MessageBox.Show("Dismiss the selected employee?", "Dismissing",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                employee.IsDismissed = true;
                employee.DateOfDismissal = DateTime.Now;
                App.DB.SaveChanges();
                MessageBox.Show("The employee has successfully been dismissed.");
            }
        }

        void ReturnDismissedEmployee(Staff employee)
        {
            if (MessageBox.Show("Return the employee to work?", "Returning",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                employee.IsDismissed = false;
                employee.DateOfDismissal = null;
                App.DB.SaveChanges();
                MessageBox.Show("The employee has successfully been returned to work.");
            }
        }
        #endregion

        #region Navigation buttons
        private void btnDoubleLeftArrow_Click(object sender, RoutedEventArgs e)
        {
            dgEmployees.SelectedIndex = 0;
        }
        private void btnLeftArrow_Click(object sender, RoutedEventArgs e)
        {
            if ((dgEmployees.SelectedItem != null) && (dgEmployees.SelectedIndex > 0))
                dgEmployees.SelectedIndex -= 1;
        }
        private void btnRightArrow_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployees.SelectedItem != null)
                dgEmployees.SelectedIndex += 1;
        }
        private void btnDoubleRightArrow_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployees.Items.Count > 0)
                dgEmployees.SelectedIndex = dgEmployees.Items.Count - 1;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text == "")
                tbSearchPlaceholder.Text = "search by full name";
            else tbSearchPlaceholder.Text = "";

            if (txtSearch.Text.Length > 1)
            {
                var search = txtSearch.Text.ToLower().Trim();
                List<Staff> employees;
                if (chkShowDismissed.IsChecked == false)
                    employees = _staff.Where(s => s.IsDismissed == false).ToList();
                else employees = _staff.Where(s => s.IsDismissed == true).ToList();
                dgEmployees.ItemsSource = employees.Where(s => s.FirstName.ToLower().Contains(search) ||
                                                               s.Surname.ToLower().Contains(search) ||
                                                               s.Patronymic.ToLower().Contains(search)); 
            }
            else
            {
                if (chkShowDismissed.IsChecked == false)
                    dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == false).ToArray();
                else dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == true).ToArray();
            }
        }

        private void chkShowDismissed_Checked(object sender, RoutedEventArgs e)
        {
            dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == true).ToArray();
            dgtxtclDateOfDismissal.Visibility = Visibility.Visible;
        }
        private void chkShowDismissed_Unchecked(object sender, RoutedEventArgs e)
        {
            dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == false).ToArray();
            dgtxtclDateOfDismissal.Visibility = Visibility.Hidden;
        }
        #endregion

        void UpdateDataGrid()
        {
            if (chkShowDismissed.IsChecked == false)
                dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == false).ToArray();
            else dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == true).ToArray();
        }
    }
}
