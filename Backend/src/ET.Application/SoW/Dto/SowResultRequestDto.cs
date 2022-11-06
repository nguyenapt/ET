using System;

namespace ET.SoW.Dto
{
    public class SowResultRequestDto
    {
        public string Name { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string BillType { get; set; }
        public int? SowNumber { get; set; }
        public decimal? Version { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Keyword { get; set; }
        public string SortBy { get; set; }
        public string ProjectTag { get; set; }
        public string Status { get; set; }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
