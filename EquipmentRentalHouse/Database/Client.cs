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
    
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Building { get; set; }
        public int StreetId { get; set; }
        public string Apartment { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public System.DateTime DateOfOrder { get; set; }
        public string Phone { get; set; }
        public string PassportIssuedBy { get; set; }
        public System.DateTime PassportDateOfIssue { get; set; }
        public System.DateTime PassportDateOfExpiration { get; set; }
        public string PassportCode { get; set; }
        public string PassportNumber { get; set; }
        public string PassportPlaceOfBirth { get; set; }
    
        public virtual Street Street { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
