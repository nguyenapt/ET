using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace ET.Users.Dto
{
    //custom PagedResultRequestDto
    public class PagedUserResultRequestDto : PagedResultRequestDto
    {
        [Range(-1,int.MaxValue)]
        public override int MaxResultCount { get; set; }
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
