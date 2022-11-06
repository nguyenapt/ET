using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ET.Entities;
using Microsoft.Extensions.Configuration;
using System.Linq.Dynamic.Core;
using System.Linq;
using Task = System.Threading.Tasks.Task;
using Microsoft.AspNetCore.Hosting;
using ET.Configuration;
using Microsoft.Extensions.Hosting;

namespace ET.DeploymentInformations
{
    public class DeploymentInformationAppService : ETAppServiceBase, IDeploymentInformationAppService
    {
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<DeploymentInformation, Guid> _repository;
        //private readonly IWebHostEnvironment _webHostEnvironment;
        public DeploymentInformationAppService(IRepository<DeploymentInformation, Guid> repository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _appConfiguration = AppConfigurations.Get(webHostEnvironment.ContentRootPath, webHostEnvironment.EnvironmentName, webHostEnvironment.IsDevelopment());
            //_webHostEnvironment = webHostEnvironment;
        }

        public object GetLatestVersion()
        {
            var deployInfo = _repository.GetAllList(x => x.EnvironmentCode.Equals(_appConfiguration["Env:Name"]));
            if (deployInfo != null)
            {
                var feVer = deployInfo.Where(a => a.ProjectCode.Equals("FE")).OrderByDescending(a => a.UpdateDate).FirstOrDefault();
                var beVer = deployInfo.Where(a => a.ProjectCode.Equals("BE")).OrderByDescending(a => a.UpdateDate).FirstOrDefault();
                var dbVer = deployInfo.Where(a => a.ProjectCode.Equals("DB")).OrderByDescending(a => a.UpdateDate).FirstOrDefault();

                return new { FE = feVer != null ? feVer.VersionNumber : "", BE = beVer != null ? beVer.VersionNumber : "", DB = dbVer != null ? dbVer.VersionNumber : "" };
            }

            return null;
        }

        //public Task<string> GetEnvironmentName()
        //{
        //    return Task.FromResult(_appConfiguration["Env:Name"]);
        //}
    }
}

