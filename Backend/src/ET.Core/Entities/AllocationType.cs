namespace ET.Entities
{
    using Abp.Domain.Entities;
    using System; 
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("AllocationType")]
    public partial class AllocationType : Entity<int>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        public bool? IsSupporter { get; set; }        
    }
}
