using System.Linq;
using System.Text;

namespace EquipmentRentalHouse.Database
{
    public partial class Client
    {
        public int TotalOrdersCount
        {
            get
            {
                return Orders.Count;
            }
        }

        public int ReturnedOrdersCount
        {
            get
            {
                return Orders.Where(o => o.IsReturned == true).Count();
            }
        }

        public string FullName
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Surname);
                sb.Append(' ');
                sb.Append(FirstName);
                sb.Append(' ');
                sb.Append(Patronymic);
                return sb.ToString();
            }
        }
    }

    public partial class Staff
    {
        public string FullName
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Surname);
                sb.Append(' ');
                sb.Append(FirstName);
                sb.Append(' ');
                sb.Append(Patronymic);
                return sb.ToString();
            }
        }
    }
}