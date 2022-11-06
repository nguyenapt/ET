using Abp.AutoMapper;

namespace ET.ProjectStateType.Dto
{
    [AutoMapTo(typeof(Entities.ProjectStateType))]
    public class CreateProjectStateTypeDto
    {
        public string State { get; set; }
    }
}
