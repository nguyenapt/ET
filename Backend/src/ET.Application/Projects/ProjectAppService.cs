using System;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using ET.Projects.Dto;
using ET.Entities;
using System.Threading.Tasks;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Abp.Linq.Extensions;
using ET.Authorization;
using ET.SoW;
using ET.SoW.Dto;
using Task = System.Threading.Tasks.Task;

namespace ET.Projects
{
    [AbpAuthorize(PermissionNames.Pages_ProjectRoles)]
    public class ProjectAppService : AsyncCrudAppService<Project, ProjectDto, Guid, ProjectResultRequestDto, CreateProjectDto, UpdateProjectDto>, IProjectAppService
    {
        private readonly ISowAppService _sowAppService;
        public ProjectAppService(IRepository<Project, Guid> repository, ISowAppService sowAppService) : base(repository)
        {
            _sowAppService = sowAppService;
            LocalizationSourceName = ETConsts.LocalizationSourceName;
        }

        public override Task<ProjectDto> CreateAsync(CreateProjectDto input)
        {
            if (DuplicatedName(input.Name,null))
            {
                return Task.FromException<ProjectDto>(new Exception(L("DuplicateProjectName")));
            }

            var projectDto = base.CreateAsync(input);

            var projectEntity = GetEntityByIdAsync(projectDto.Result.Id);
            projectEntity.Result.ProjectCode = AlphaNumericStringGenerator(projectEntity.Result.UniqueCode, 36);
            var updateProject = ObjectMapper.Map<UpdateProjectDto>(projectEntity.Result);

            return UpdateAsync(updateProject);
        }

        public override Task<ProjectDto> UpdateAsync(UpdateProjectDto input)
        {
            if (DuplicatedName(input.Name, input.Id))
            {
                return Task.FromException<ProjectDto>(new Exception(L("DuplicateProjectName")));
            }
            return base.UpdateAsync(input);
        }

        private bool DuplicatedName(string projectName, Guid? projectId)
        {
            var items = Repository.GetAll().AsEnumerable()
                .WhereIf(projectId.HasValue, x => x.Id != projectId.Value)
                .Where(x => x.Name.Equals(projectName, StringComparison.InvariantCultureIgnoreCase));
            return items.Any();
        }

        /// <summary>
        /// Converts the given decimal number to the numeral system with the
        /// specified radix (in the range [2, 36]).
        /// </summary>
        /// <param name="decimalNumber">The number to convert.</param>
        /// <param name="radix">The radix of the destination numeral system (in the range [2, 36]).</param>
        /// <returns></returns>

        private string AlphaNumericStringGenerator(int decimalNumber, int radix)
        {
            const int bitsInLong = 64;
            const string digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (radix < 2 || radix > digits.Length)
                throw new ArgumentException("The radix must be >= 2 and <= " + digits.Length);

            if (decimalNumber == 0)
                return "0";

            int index = bitsInLong - 1;
            long currentNumber = Math.Abs(decimalNumber);
            char[] charArray = new char[bitsInLong];

            while (currentNumber != 0)
            {
                int remainder = (int)(currentNumber % radix);
                charArray[index--] = digits[remainder];
                currentNumber /= radix;
            }

            var result = $"00{new string(charArray, index + 1, bitsInLong - index - 1)}";

            return result.Substring(result.Length - 3);
        }
        protected override IQueryable<Project> ApplySorting(IQueryable<Project> query, ProjectResultRequestDto input)
        {
            return query.OrderByDescending(x => x.CreationTime);
        }

        public override Task DeleteAsync(EntityDto<Guid> input)
        {
            var projectUsedInSow = _sowAppService.GetAllAsync(new SowResultRequestDto
            {
                ProjectId = input.Id,
                PageSize = 1,
                CurrentPage = 1
            }).Result.TotalCount;
            return projectUsedInSow > 0 ? Task.FromException(new Exception(L("ItemIsUsedInSow"))) : base.DeleteAsync(input);
        }

        protected override IQueryable<Project> CreateFilteredQuery(ProjectResultRequestDto input)
        {
            return Repository.GetAllIncluding(
                    x => x.Client, 
                    x=> x.Department, 
                    x=> x.ProjectType,
                    x=> x.PMOResource,
                    x=> x.ProjectManager,
                    x=> x.ProjectState)
                .WhereIf(input.ClientId.HasValue, x=> x.ClientId == input.ClientId.Value)
                .WhereIf(input.DepartmentId.HasValue, x=> x.DepartmentId == input.DepartmentId.Value)
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), x=> x.Name.Contains(input.Name))
                .WhereIf(input.ProjectTypeId.HasValue, x=> x.ProjectTypeId == input.ProjectTypeId.Value)
                .WhereIf(!input.ProjectCode.IsNullOrWhiteSpace(), x=> x.ProjectCode.Contains(input.ProjectCode))
                .WhereIf(input.PMOId.HasValue, x=> x.PMOId == input.PMOId.Value)
                .WhereIf(input.ProjectManagerId.HasValue, x=>x.ProjectManagerId == input.ProjectManagerId.Value)
                .WhereIf(input.ProjectStateId.HasValue, x=> x.ProjectStateId == input.ProjectStateId.Value);
        }

        protected override Task<Project> GetEntityByIdAsync(Guid id)
        {
            var project = Repository.GetAllIncluding(
                    x => x.Department,
                    x => x.Client, 
                    x=> x.ProjectType,
                    x=> x.ProjectManager,
                    x=> x.PMOResource,
                    x=> x.ProjectState)
                .FirstOrDefault(x => x.Id == id);
            return Task.FromResult(project);
        }
    }
}

