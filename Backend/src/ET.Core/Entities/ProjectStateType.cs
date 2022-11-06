using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ET.Entities
{
    [Table("ProjectStateType")]
    public partial class ProjectStateType : Entity<int>
    {
        public string State { get; set; }
    }
}
