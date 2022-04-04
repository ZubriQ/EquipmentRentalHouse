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

        // Editing.
        public StaffWindow(Staff employee)
        {
            InitializeComponent();
            InitializeEditing();

            _employee = employee;
            DataContext = _employee;
        }

        // Adding.
        public StaffWindow()
        {
            InitializeComponent();
            InitializeAdding();
        }

        void InitializeEditing()
        {
            lblTitle.Content = "Equipment Rental House - Editing an existing employee";
            btnConfirm.Content = "Save";
            cbPosition.ItemsSource = App.DB.Positions.Local.ToArray();
            cbStreet.ItemsSource = App.DB.Streets.Local.ToArray();
        }

        void InitializeAdding()
        {
            lblTitle.Content = "Equipment Rental House - Adding a new employee";
            btnConfirm.Content = "Add";
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
    }
}
