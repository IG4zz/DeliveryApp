//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeliveryApp.DBEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Package_Movings
    {
        public int Move_ID { get; set; }
        public int Package_ID { get; set; }
        public int MoveType_ID { get; set; }
        public System.DateTime MoveDate { get; set; }
    
        public virtual Move_Types Move_Types { get; set; }
        public virtual Packages Packages { get; set; }
    }
}