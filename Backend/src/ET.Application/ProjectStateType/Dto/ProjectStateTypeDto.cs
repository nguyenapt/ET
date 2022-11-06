using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace ET.ProjectStateType.Dto
{
    [AutoMapTo(typeof(Entities.ProjectStateType))]
    [AutoMapFrom(typeof(Entities.ProjectStateType))]
    public class ProjectStateTypeDto : EntityDto<int>
    {
        public string State { get; set; }
    }
}
