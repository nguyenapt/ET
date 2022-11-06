using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using ET.Departments.Dto;
using ET.Entities;
using ET.Projects;
using ET.Projects.Dto;
using Task = System.Threading.Tasks.Task;

namespace ET.Departments
{
    public class DepartmentAppService : AsyncCrudAppService<Department, DepartmentDto, Guid, DepartmentResultRequestDto, CreateDepartmentDto, DepartmentDto>, IDepartmentAppService
    {
        private readonly IProjectAppService _projectAppService;
        public DepartmentAppService(IRepository<Department, Guid> repository, IProjectAppService projectAppService) : base(repository)
        {
            _projectAppService = projectAppService;
            LocalizationSourceName = ETConsts.LocalizationSourceName;
        }

        protected override IQueryable<Department> CreateFilteredQuery(DepartmentResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
                .WhereIf(!input.DepartmentCode.IsNullOrWhiteSpace(), x => x.DepartmentCode.Contains(input.DepartmentCode));
        }

        public override Task DeleteAsync(EntityDto<Guid> input)
        {
            var clientUsedInProject = _projectAppService.GetAllAsync(new ProjectResultRequestDto
            {
                DepartmentId = input.Id,
                PageSize = 1,
                CurrentPage = 1
            }).Result.TotalCount;

            if (clientUsedInProject > 0) return Task.FromException(new Exception(L("ItemIsUsedInProject")));

            return base.DeleteAsync(input);
        }
    }
}

