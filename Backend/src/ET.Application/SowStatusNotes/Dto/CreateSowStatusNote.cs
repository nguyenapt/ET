using Abp.AutoMapper;
using ET.Entities;

namespace ET.SowStatusNotes.Dto
{
    [AutoMapTo(typeof(SowStatusNote))]
    [AutoMapFrom(typeof(SowStatusNote))]
    public class CreateSowStatusNote
    {
        public string Status { get; set; }
        public string StatusNote { get; set; }
    }
}
