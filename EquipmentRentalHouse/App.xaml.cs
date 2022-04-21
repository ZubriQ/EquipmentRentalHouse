using EquipmentRentalHouse.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EquipmentRentalHouse
{
    public partial class App : Application
    {
        public static ERHEntities DB = new ERHEntities();

        public static User User { get; set; } // Current user.
        public static ObjectsUser Rights { get; set; } // Rights.
    }
}
