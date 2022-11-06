using System;

namespace ET.Projects.Dto
{
    public class ProjectResultRequestDto
    {
        public Guid? ClientId { get; set; }
        public Guid? DepartmentId { get; set; }
        public string Name { get; set; }
        public int? ProjectTypeId { get; set; }
        public string ProjectCode { get; set; }
        public Guid? ProjectManagerId { get; set; }
        public Guid? PMOId { get; set; }
        public int? ProjectStateId { get; set; }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
