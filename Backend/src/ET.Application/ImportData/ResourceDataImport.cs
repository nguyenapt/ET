using Abp.Domain.Repositories;
using ET.ImportData.Dto;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using ET.Authorization.Users;
using ET.Entities;
using System.Threading.Tasks;

namespace ET.ImportData
{
    public class ResourceDataImport : ImportDataBaseAppService<Resource, Guid, ResourceImportData>
    {
        private readonly UserManager _userManager;
        protected readonly IRepository<Department, Guid> _departmentRepository;
        protected readonly IRepository<WorkingHourRule, Guid> _workingHourRuleRepository;
        public ResourceDataImport(IRepository<Entities.Resource, 
            Guid> repository, 
            UserManager userManager,
            IRepository<Department, Guid> departmentRepository,
            IRepository<WorkingHourRule, Guid> workingHourRule) : base(repository)
        {
            _userManager = userManager;
            _departmentRepository = departmentRepository;
            _workingHourRuleRepository = workingHourRule;
        }

        public override IEnumerable<T> MappingData<T>(IFormFile file) 
        {
            var dtos = base.MappingData<ResourceImportData>(file);
            var result = new List<ResourceImportDto>();
            
            foreach(var item in dtos)
            {
                var resource = new ResourceImportDto
                {
                    FirstName = item.FirstName?.Trim(),
                    LastName = item.LastName?.Trim(),
                    UserName = item.UserName?.Trim(),
                    Country = item.Country?.Trim(),
                    EmployeeCode = item.EmployeeCode?.Trim(),
                    DepartmentName = item.DepartmentName?.Trim(),
                    WorkingHourRuleName = item.WorkingHourRuleName?.Trim(),
                    JobTitle = item.JobTitle?.Trim(),
                    JobTitleLevel = item.JobTitleLevel?.Trim()
                };

                var data = CompleteData(resource);
                if (data != null)
                    result.Add(data);
            }
            return result as List<T>;
        }

        public override Task<object> ImportDataAsync(IFormFile file)
        {
            var dataDtos = MappingData<ResourceImportDto>(file);

            if (dataDtos == null || !dataDtos.Any())
            {
                return System.Threading.Tasks.Task.FromResult<object>(new
                {
                    success = false,
                    error = "Please re-check file imported or data",
                    message = "Please re-check file imported or data"
                });
            }
        
            var oldCurrentEntities = _repository.GetAll()?.ToList();

            // Updating resources without username
            var resourcesWithoutUserName = oldCurrentEntities.Where(x => string.IsNullOrEmpty(x.UserName)).ToList();
            if (resourcesWithoutUserName != null && resourcesWithoutUserName.Any())
            {
                foreach (var resource in resourcesWithoutUserName)
                {
                    if (resource.UserId.HasValue)
                    {
                        var user = _userManager.GetUserById(resource.UserId.Value);
                       
                        if (user != null)
                        {
                            resource.UserName = user.UserName;
                            _repository.UpdateAsync(resource);
                        }
                    }
                }
            }

            var currentEntities = _repository.GetAll()?.ToList();
            var updatedRow = 0;
            var newRow = 0;
            var resourceErrorList = new List<ResourceImportDto>();
            foreach (var dto in dataDtos)
            {
                var exist = currentEntities.Where(x => !string.IsNullOrEmpty(x.UserName)).
                    FirstOrDefault(x => x.UserName.Equals(dto.UserName, 
                    StringComparison.InvariantCultureIgnoreCase));
                if (exist != null)
                {
                    exist.FirstName = !string.IsNullOrEmpty(dto.FirstName) ? dto.FirstName : exist.FirstName;
                    exist.LastName = !string.IsNullOrEmpty(dto.LastName) ? dto.LastName : exist.LastName;
                    exist.EmployeeCode = !string.IsNullOrEmpty(dto.EmployeeCode) ? dto.EmployeeCode : exist.EmployeeCode;
                    exist.Country = !string.IsNullOrEmpty(dto.Country) ? dto.Country : exist.Country;
                    exist.DepartmentId = dto.DepartmentId;
                    exist.WorkingHourRuleId = dto.WorkingHourRuleId;
                    exist.UserName = dto.UserName;
                    updatedRow++;
                    _repository.UpdateAsync(exist);
                }
                else
                {
                    var createInput = ObjectMapper.Map<Resource>(dto);
                    newRow++;
                    _repository.InsertAsync(createInput);
                }
            }
          
            return System.Threading.Tasks.Task.FromResult<object>(new
            {
                success = true,
                error = "",
                message = $"{newRow}/{dataDtos.Count()} resources are imported successfully " +
                $"and {updatedRow}/{dataDtos.Count()} resources are updated successfully"
            });
        }
        
        protected ResourceImportDto CompleteData(ResourceImportDto dto)
        {
            if (!string.IsNullOrEmpty(dto.UserName))
            {
                UpdateDepartmentAndWorkingHourRule(dto);
                return dto;
            }
            return null;
        }

        private void UpdateDepartmentAndWorkingHourRule(ResourceImportDto dto)
        {
            if (!string.IsNullOrEmpty(dto.DepartmentName))
            {
                var deparment = _departmentRepository.GetAll().ToList().FirstOrDefault(x => x.Name.Equals(dto.DepartmentName,
                    StringComparison.InvariantCultureIgnoreCase));
                if (deparment != null)
                {
                    dto.DepartmentId = deparment.Id;
                }
                else
                {
                    var newDepartment = new Department()
                    {
                        Name = dto.DepartmentName
                    };
                    var id = _departmentRepository.InsertAndGetId(newDepartment);
                    CurrentUnitOfWork.SaveChanges();
                    dto.DepartmentId = id;
                }
            }
            if (!string.IsNullOrEmpty(dto.WorkingHourRuleName))
            {
                var workingHourRule = _workingHourRuleRepository.GetAll().ToList().FirstOrDefault(x => x.Name.Equals(dto.WorkingHourRuleName
                    , StringComparison.InvariantCultureIgnoreCase));
                if (workingHourRule != null)
                {
                    dto.WorkingHourRuleId = workingHourRule.Id;
                }
                else
                {
                    var newworkingHourRule = new WorkingHourRule()
                    {
                        Name = dto.WorkingHourRuleName
                    };
                    var id = _workingHourRuleRepository.InsertAndGetId(newworkingHourRule);
                    CurrentUnitOfWork.SaveChanges();
                    dto.WorkingHourRuleId = id;
                }
            }
        }
    }
}
