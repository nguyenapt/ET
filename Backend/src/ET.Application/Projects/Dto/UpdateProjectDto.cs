using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.Projects.Dto
{
    [AutoMapFrom(typeof(Project))]
    [AutoMapTo(typeof(Project))]
    public class UpdateProjectDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ProjectTypeId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? ProjectManagerId { get; set; }
        public Guid? PMOId { get; set; }
        public string Description { get; set; }
        [StringLength(65)]
        public string ProjectTag { get; set; }
        public int? ProjectStateId { get; set; }
    }
}
