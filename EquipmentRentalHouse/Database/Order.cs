//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EquipmentRentalHouse.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int ClientId { get; set; }
        public int StockKeepingUnitId { get; set; }
        public System.DateTime DateOfOrder { get; set; }
        public System.DateTime DateOfExpiration { get; set; }
        public bool IsReturned { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual StockKeepingUnit StockKeepingUnit { get; set; }
    }
}