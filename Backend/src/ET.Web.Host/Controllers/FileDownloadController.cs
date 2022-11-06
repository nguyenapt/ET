using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ET.Controllers;
using ET.SoW;
using System;
using Abp.Authorization;
using ET.Authorization;

namespace ET.Web.Host.Controllers
{
    [AbpAuthorize]
    public class FileDownloadController : ETControllerBase
    {
        private readonly ISowAppService _sowAppService;

        public FileDownloadController(ISowAppService sowAppService)
        {
            _sowAppService = sowAppService;
        }

        [AbpAuthorize(PermissionNames.Pages_SoWs)]
        public async Task<FileResult> SoWXlsx(Guid id)
        {
            var fileResult = await _sowAppService.GetSoWExportDataAsync(id);

            return File(fileResult.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileResult.FileName);
        }
    }
}
