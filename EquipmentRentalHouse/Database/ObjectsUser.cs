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
    
    public partial class ObjectsUser
    {
        public string UserLogin { get; set; }
        public int ObjectId { get; set; }
        public bool C { get; set; }
        public bool R { get; set; }
        public bool U { get; set; }
        public bool D { get; set; }
    
        public virtual Object Object { get; set; }
        public virtual User User { get; set; }
    }
}