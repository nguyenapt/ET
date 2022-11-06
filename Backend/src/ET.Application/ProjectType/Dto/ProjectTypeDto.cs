using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace ET.ProjectType.Dto
{
    [AutoMapFrom(typeof(Entities.ProjectType))]
    [AutoMapTo(typeof(Entities.ProjectType))]
    public class ProjectTypeDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Scope { get; set; }
        public bool PL { get; set; }
    }
}
