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
    public partial class Employees : UserControl
    {
        List<Staff> _staff;

        public Employees()
        {
            InitializeComponent();
            _staff = App.DB.Staffs.ToList();
            dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == false).ToArray();

            dgEmployees.Columns[9].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }


        #region Add, Edit, Dismiss controls
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            StaffAddEditWindow window = new StaffAddEditWindow();
            window.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployees.SelectedItem != null)
            {
                var employee = dgEmployees.SelectedItem as Staff;
                StaffAddEditWindow window = new StaffAddEditWindow(employee);
                window.ShowDialog();
            }
        }

        private void btnDismiss_Click(object sender, RoutedEventArgs e)
        {
            var employee = dgEmployees.SelectedItem as Staff;
            if ((employee != null) && (employee.IsDismissed == false))
            {
                DismissEmployee(employee);
                Update();
            }
            else if ((employee != null) && (employee.IsDismissed == true))
            {
                ReturnDismissedEmployee(employee);
                Update();
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            _staff = App.DB.Staffs.ToList();
            if (chkShowDismissed.IsChecked == false)
                dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == false).ToArray();
            else dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == true).ToArray();
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

        // Employment history.
        private void btnEmploymentHistory_Click(object sender, RoutedEventArgs e)
        {
            var employee = dgEmployees.SelectedItem as Staff;
            if (employee != null)
            {
                EmploymentHistoryWindow window = new EmploymentHistoryWindow(employee);
                window.ShowDialog();
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchbarPlaceholderState();

            if (txtSearch.Text.Length > 1)
                SearchEmployee();
            else
                EmployeeNotFound();
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
            List<Staff> employees;
            if (chkShowDismissed.IsChecked == false)
                employees = _staff.Where(s => s.IsDismissed == false).ToList();
            else employees = _staff.Where(s => s.IsDismissed == true).ToList();
            try
            {
                dgEmployees.ItemsSource = employees.Where(s => s.FullName.ToLower().Contains(search));
                //dgEmployees.ItemsSource = employees.Where(s => s.FirstName.ToLower().Contains(search) ||
                //                                          s.Surname.ToLower().Contains(search));
                //  || s.Patronymic.ToLower().Contains(search)
            }
            catch { }
        }

        void EmployeeNotFound()
        {
            if (chkShowDismissed.IsChecked == false)
                dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == false).ToArray();
            else dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == true).ToArray();
        }

        private void chkShowDismissed_Checked(object sender, RoutedEventArgs e)
        {
            dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == true).ToArray();
            dgtxtclDateOfDismissal.Visibility = Visibility.Visible;
            dgEmployees.Columns[9].Width = new DataGridLength(1, DataGridLengthUnitType.Auto);
        }
        private void chkShowDismissed_Unchecked(object sender, RoutedEventArgs e)
        {
            dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == false).ToArray();
            dgtxtclDateOfDismissal.Visibility = Visibility.Hidden;
            dgEmployees.Columns[9].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        #endregion

        void Update()
        {
            if (chkShowDismissed.IsChecked == false) // TODO: Update from App.DB?
                dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == false).ToArray();
            else dgEmployees.ItemsSource = _staff.Where(s => s.IsDismissed == true).ToArray();
        }
    }
}