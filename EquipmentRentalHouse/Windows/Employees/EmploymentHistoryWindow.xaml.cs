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
    public partial class EmploymentHistoryWindow : Window
    {
        Staff _employee;
        List<EmploymentHistory> _employmentHistories = new List<EmploymentHistory>();

        public EmploymentHistoryWindow(Staff employee)
        {
            InitializeComponent();
            _employee = employee;
            InitializeWindow();
        }

        void InitializeWindow()
        {
            lblFirstname.Content = _employee.FirstName;
            lblSurname.Content = _employee.Surname;
            lblPatronymic.Content = _employee.Patronymic;
            _employmentHistories = App.DB.EmploymentHistories.Where(x => x.Id == _employee.Id).ToList();
            dgEmployees.ItemsSource = _employmentHistories;
        }

        private void brdBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
