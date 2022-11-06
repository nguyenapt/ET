using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Clients.Dto;
using ET.Departments.Dto;
using ET.Entities;
using ET.ProjectStateType.Dto;
using ET.ProjectType.Dto;
using ET.Resources.Dto;

namespace ET.Projects.Dto
{
    [AutoMapFrom(typeof(Project))]
    [AutoMapTo(typeof(Project))]
    public class ProjectDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? ClientId { get; set; }
        public int? ProjectTypeId { get; set; }
        public Guid? ProjectManagerId { get; set; }
        public Guid? PMOId { get; set; }
        public int UniqueCode { get; set; }
        public string ProjectTag { get; set; }
        public string ProjectCode { get; set; }
        public int? ProjectStateId { get; set; }
        public virtual ClientDto Client { get; set; }
        public virtual DepartmentDto Department { get; set; }
        public virtual ProjectTypeDto ProjectType { get; set; }
        public ResourceDto PMOResource { get; set; }
        public ResourceDto ProjectManager { get; set; }
        public ProjectStateTypeDto ProjectState { get; set; }
    }
}
