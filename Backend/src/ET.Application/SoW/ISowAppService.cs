using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using ET.Shared.Dto;
using ET.SoW.Dto;

namespace ET.SoW
{
    public interface ISowAppService : IAsyncCrudAppService<SowDto, Guid, SowResultRequestDto, CreateSowDto, UpdateSowDto>
    {
        Task<FileResultDto> GetSoWExportDataAsync(Guid id);
        Task<SowDto> CreateNewVersion(Guid parentId);
    }
}


