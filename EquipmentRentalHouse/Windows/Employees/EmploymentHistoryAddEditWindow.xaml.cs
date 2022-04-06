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
    public partial class EmploymentHistoryAddEditWindow : Window
    {
        EmploymentHistory _history;
        bool _isAdding = false;

        // Adding.
        public EmploymentHistoryAddEditWindow(int _emplId)
        {
            InitializeComponent();
            InitializeWindow();
            _isAdding = true;
            lblTitle.Content = @"Employment history - Adding";
            btnConfirm.Content = "Add";

            _history = new EmploymentHistory();
            _history.DateOfOrder = DateTime.Now;
            _history.StaffId = _emplId;
            DataContext = _history;
        }

        // Editing.
        public EmploymentHistoryAddEditWindow(EmploymentHistory eh)
        {
            InitializeComponent();
            InitializeWindow();
            lblTitle.Content = @"Employment history - Editing";
            btnConfirm.Content = "Edit";

            _history = eh;
            DataContext = _history;
        }

        void InitializeWindow()
        {
            dpDateOfOrder.SelectedDate = DateTime.Now;
            cbPosition.ItemsSource = App.DB.Positions.ToList();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void brdBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

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
                App.DB.EmploymentHistories.Add(_history);
                App.DB.SaveChanges();
                MessageBox.Show($"New employment history has been successfully added.");
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
                MessageBox.Show($"The history has been successfully changed.");
            }
            catch { }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
