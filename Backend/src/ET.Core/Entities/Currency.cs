using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ET.Entities
{
    [Table("Currency")]
    public partial class Currency : Entity<int>
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
