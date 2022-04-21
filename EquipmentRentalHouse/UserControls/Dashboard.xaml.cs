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
using System.Windows.Threading;

namespace EquipmentRentalHouse.UserControls
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
            UpdateData();
        }

        void UpdateData()
        {
            txtEmployeesTotal.Text = App.DB.Staffs.Count().ToString();
            txtEmployeesHired.Text = App.DB.Staffs.Where(x => x.IsDismissed == false).Count().ToString();
            txtDismissedStaff.Text = App.DB.Staffs.Where(x => x.IsDismissed == true).Count().ToString();
            txtClientsTotal.Text = App.DB.Staffs.Count().ToString();
            txtDevicesTotal.Text = App.DB.StockKeepingUnits.Count().ToString();
            txtDevicesInStock.Text = App.DB.StockKeepingUnits.Where(x => x.IsInStock == true).Count().ToString();
            txtDevicesRented.Text = App.DB.StockKeepingUnits.Where(x => x.IsInStock == false).Count().ToString();
        }
    }
}
