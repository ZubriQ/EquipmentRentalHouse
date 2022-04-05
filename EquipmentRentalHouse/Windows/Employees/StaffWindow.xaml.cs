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

namespace EquipmentRentalHouse.Windows.Employees
{
    /// <summary>
    /// To either add or edit an employee.
    /// </summary>
    public partial class StaffWindow : Window
    {
        private Staff _employee;

        private bool _isAdding;

        // Editing.
        public StaffWindow(Staff employee)
        {
            InitializeComponent();
            InitializeComboBoxes();
            InitializeEditing(employee);
        }

        // Adding.
        public StaffWindow()
        {
            InitializeComponent();
            InitializeComboBoxes();
            InitializeAdding();
        }

        void InitializeEditing(Staff employee)
        {
            lblTitle.Content = "Equipment Rental House - Editing an existing employee";
            btnConfirm.Content = "Save";
            _isAdding = false;

            _employee = employee;
            DataContext = _employee;
        }

        void InitializeAdding()
        {
            lblTitle.Content = "Equipment Rental House - Adding a new employee";
            btnConfirm.Content = "Add";
            _isAdding = true;

            _employee = new Staff();
            DataContext = _employee;
        }

        void InitializeComboBoxes()
        {
            cbPosition.ItemsSource = App.DB.Positions.Local.ToArray();
            cbStreet.ItemsSource = App.DB.Streets.Local.ToArray();
        }

        private void brdBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Finish adding or editing.
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
                App.DB.Staffs.Add(_employee);
                App.DB.SaveChanges();
                MessageBox.Show($"The employee has been successfully added.");
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
                MessageBox.Show($"The employee has been successfully changed.");
            }
            catch { }
        }
    }
}
