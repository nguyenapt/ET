using Abp.Domain.Entities.Auditing;

namespace ET.Entities
{
    using Abp.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("DeploymentInformation")]
    public partial class DeploymentInformation : Entity<Guid>
    {
        public DeploymentInformation()
        {
        }
        
        public DateTime UpdateDate { get; set; }
        public string VersionNumber { get; set; }
        public string EnvironmentCode { get; set; }
        public string ProjectCode { get; set; }
        public string Description { get; set; }
        
    }
}
