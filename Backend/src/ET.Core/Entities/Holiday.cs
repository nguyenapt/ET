namespace ET.Entities
{
    using Abp.Domain.Entities;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("Holiday")]
    public partial class Holiday : Entity<Guid>
    {
        public string Country { get; set; }

        [Column(TypeName = "date")]
        public DateTime HolidayDate { get; set; }

        [StringLength(50)]
        public string HolidayName { get; set; }
    }
}
