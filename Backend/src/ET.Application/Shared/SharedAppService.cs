using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using ET.Authorization.Roles;
using ET.Entities;
using ET.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace ET.Shared
{
    [Authorize]
    public class SharedAppService : ApplicationService, ISharedAppService
    {
        protected readonly IRepository<Allocation, Guid> _allocationRepository;
        protected readonly IRepository<Beneficiary, Guid> _beneficiaryRepository;
        protected readonly IRepository<Client, Guid> _clientRepository;
        protected readonly IRepository<Department, Guid> _departmentRepository;
        protected readonly IRepository<Holiday, Guid> _holidayRepository;
        protected readonly IRepository<InvoiceInfo, Guid> _invoiceInfoRepository;
        protected readonly IRepository<LeavePermission, Guid> _leavePermissionRepository;
        protected readonly IRepository<LeaveType, Guid> _leaveTypeRepository;
        protected readonly IRepository<Project, Guid> _projectRepository;
        protected readonly IRepository<Resource, Guid> _resourceRepository;
        protected readonly IRepository<ResourceSkill, Guid> _resourceSkillRepository;
        protected readonly IRepository<Skill, Guid> _skillRepository;
        protected readonly IRepository<SOW, Guid> _sowRepository;
        protected readonly IRepository<SOWRole, Guid> _sowRoleRepository;
        protected readonly IRepository<Task, Guid> _taskRepository;
        protected readonly IRepository<TaskCategory, Guid> _taskCategoryRepository;
        protected readonly IRepository<TimesheetEntry, Guid> _timesheetEntryRepository;
        protected readonly IRepository<WorkingHourRule, Guid> _workingHourRuleRepository;
        protected readonly IRepository<Entities.BillingRate, int> _billingRateRepository;
        protected readonly IRepository<BillingType, int> _billingTypeRepository;
        protected readonly IRepository<Entities.Currency, int> _currencyRepository;
        protected readonly IRepository<RateType, int> _rateTypeRepository;
        protected readonly IRepository<ResourceRole, int> _resourceRoleRepository;
        protected readonly IRepository<Role, int> _roleRepository;
        protected readonly IRepository<Entities.ProjectType, int> _projectTypeRepository;
        protected readonly IRepository<SkillLevel, Guid> _skillLevelRepository;
        protected readonly IRepository<InternalType, Guid> _internalTypeRepository;
        private readonly RoleManager _roleManager;

        public SharedAppService(
            IRepository<Allocation, Guid> allocationRepository,
            IRepository<Beneficiary, Guid> beneficiaryRepository,
            IRepository<Client, Guid> clientRepository,
            IRepository<Department, Guid> departmentRepository,
            IRepository<Holiday, Guid> holidayRepository,
            IRepository<InvoiceInfo, Guid> invoiceInfoRepository,
            IRepository<LeavePermission, Guid> leavePermissionRepository,
            IRepository<LeaveType, Guid> leaveTypeRepository,
            IRepository<Project, Guid> projectRepository,
            IRepository<Resource, Guid> resourceRepository,
            IRepository<ResourceSkill, Guid> resourceSkillRepository,
            IRepository<Skill, Guid> skillRepository,
            IRepository<SOW, Guid> sowRepository,
            IRepository<SOWRole, Guid> sowRoleRepository,
            IRepository<Task, Guid> taskRepository,
            IRepository<TaskCategory, Guid> taskCategoryRepository,
            IRepository<TimesheetEntry, Guid> timesheetEntryRepository,
            IRepository<WorkingHourRule, Guid> workingHourRuleRepository,
            IRepository<Entities.BillingRate, int> billingRateRepository,
            IRepository<BillingType, int> billingTypeRepository,
            IRepository<Entities.Currency, int> currencyRepository,
            IRepository<RateType, int> rateTypeRepository,
            IRepository<ResourceRole, int> resourceRoleRepository,
            IRepository<Role, int> roleRepository,
            IRepository<Entities.ProjectType, int> projectTypeRepository,
            RoleManager roleManager, 
            IRepository<SkillLevel, Guid> skillLevelRepository,
            IRepository<InternalType, Guid> internalTypeRepository)
        {
            _allocationRepository = allocationRepository;
            _beneficiaryRepository = beneficiaryRepository;
            _clientRepository = clientRepository;
            _departmentRepository = departmentRepository;
            _holidayRepository = holidayRepository;
            _invoiceInfoRepository = invoiceInfoRepository;
            _leavePermissionRepository = leavePermissionRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _projectRepository = projectRepository;
            _resourceRepository = resourceRepository;
            _resourceSkillRepository = resourceSkillRepository;
            _skillRepository = skillRepository;
            _sowRepository = sowRepository;
            _sowRoleRepository = sowRoleRepository;
            _taskRepository = taskRepository;
            _taskCategoryRepository = taskCategoryRepository;
            _timesheetEntryRepository = timesheetEntryRepository;
            _workingHourRuleRepository = workingHourRuleRepository;
            _currencyRepository = currencyRepository;
            _billingRateRepository = billingRateRepository;
            _billingTypeRepository = billingTypeRepository;
            _rateTypeRepository = rateTypeRepository;
            _resourceRoleRepository = resourceRoleRepository;
            _roleRepository = roleRepository;
            _projectTypeRepository = projectTypeRepository;
            _roleManager = roleManager;
            _skillLevelRepository = skillLevelRepository;
            _internalTypeRepository = internalTypeRepository;
        }

        [HttpPost]
        public Dictionary<string, IEnumerable<KeyValueDto>> GetFormSelectors(List<string> selectors)
        {
            var formSelectorsDto = new Dictionary<string, IEnumerable<KeyValueDto>>();

            if (selectors == null || !EnumerableExtensions.Any(selectors)) return formSelectorsDto;

            foreach (var selector in selectors)
            {
                switch (selector)
                {
                    case FormSelectorConsts.Allocation:
                        GetAllocationSelector(formSelectorsDto);
                        break;
                    case FormSelectorConsts.Beneficiary:
                        GetBeneficiariesSelector(formSelectorsDto);
                        break;
                    case FormSelectorConsts.Client:
                        GetClientSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.Department:
                        GetDepartmentSelector(formSelectorsDto);
                        break;
                    case FormSelectorConsts.Holiday:
                        GetHolidaySelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.InvoiceInfo:
                        GetInvoiceInfoSelector(formSelectorsDto);
                        break;
                    case FormSelectorConsts.LeaveType:
                        GetLeaveTypeSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.Project:
                        GetProjectSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.Resource:
                        GetResourceSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.Skill:
                        GetSkillSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.SOW:
                        GetSowSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.SOWRole:
                        GetSowRoleSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.Task:
                        GetTaskSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.TaskCategory:
                        GetTaskCategorySelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.TimesheetEntry:
                        GetTimesheetEntrySelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.WorkingHourRule:
                        GetWorkingHourRuleSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.BillingType:
                        GetBillingTypeSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.Currency:
                        GetCurrencySelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.RateType:
                        GetRateTypeSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.ResourceRole:
                        GetResourceRoleSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.ProjectType:
                        GetProjectTypeSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.PMOResource:
                        GetPMOResourceSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.KAMResource:
                        GetKAMResourceSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.PMResource:
                        GetPMResourceSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.SkillLevel:
                        GetSkillLevelSelectors(formSelectorsDto);
                        break;
                    case FormSelectorConsts.InternalType:
                        GetInternalTypeSelectors(formSelectorsDto);
                        break;
                }
            }

            return formSelectorsDto;
        }

        private void GetInternalTypeSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var internalTypes = _internalTypeRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Name,
                Value = x.Id.ToString().ToLower()
            });
            formSelectorsDto.Add(FormSelectorConsts.InternalType, internalTypes);
        }

        private void GetSkillLevelSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var skillLevels = _skillLevelRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Level,
                Value = x.Id.ToString().ToLower()
            });
            formSelectorsDto.Add(FormSelectorConsts.SkillLevel, skillLevels);
        }

        private void GetPMResourceSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var pms = new List<KeyValueDto>();
            var pmoRole = _roleManager.Roles.FirstOrDefault(x => x.Name.Equals("PM"));
            if (pmoRole != null)
            {
                pms = _resourceRepository.GetAllIncluding(x => x.User, x => x.User.Roles).Where(x => x.User.Roles.Any(y => y.RoleId == pmoRole.Id)).Select(x => new KeyValueDto
                {
                    Name = $"{x.FirstName} {x.LastName}",
                    Value = x.Id.ToString().ToLower()
                }).ToList();
            }

            formSelectorsDto.Add(FormSelectorConsts.PMResource, pms);
        }

        private void GetKAMResourceSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var kams = _resourceRepository.GetAllList().Where(y => y.IsKAM).Select(x => new KeyValueDto
            {
                Name = $"{x.FirstName} {x.LastName}",
                Value = x.Id.ToString().ToLower()
            });
            formSelectorsDto.Add(FormSelectorConsts.KAMResource, kams);
        }

        private void GetPMOResourceSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var pmos = new List<KeyValueDto>();
            var pmoRole = _roleManager.Roles.FirstOrDefault(x => x.Name.Equals("PMO"));
            if (pmoRole != null)
            {
                pmos = _resourceRepository.GetAllIncluding(x => x.User, x => x.User.Roles).Where(x => x.User.Roles.Any(y => y.RoleId == pmoRole.Id)).Select(x => new KeyValueDto
                {
                    Name = $"{x.FirstName} {x.LastName}",
                    Value = x.Id.ToString().ToLower()
                }).ToList();
            }
            
            formSelectorsDto.Add(FormSelectorConsts.PMOResource, pmos);
        }

        private void GetProjectTypeSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var resourceRoles = _projectTypeRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Name,
                Value = x.Id.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.ProjectType, resourceRoles);
        }

        private void GetResourceRoleSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var resourceRoles = _resourceRoleRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Name,
                Value = x.Value?.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.ResourceRole, resourceRoles);
        }

        private void GetRateTypeSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var rateTypes = _rateTypeRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Name,
                Value = x.Value?.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.RateType, rateTypes);
        }

        private void GetCurrencySelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var currencies = _currencyRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Name,
                Value = x.Value?.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.Currency, currencies);
        }

        private void GetBillingTypeSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var billingTypes = _billingTypeRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Name,
                Value = x.Value?.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.BillingType, billingTypes);
        }

        private void GetWorkingHourRuleSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var workingHourRules = _workingHourRuleRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Name,
                Value = x.Id.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.WorkingHourRule, workingHourRules);
        }

        private void GetTimesheetEntrySelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var timesheetEntries = _timesheetEntryRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.TicketName,
                Value = x.Id.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.TimesheetEntry, timesheetEntries);
        }

        private void GetTaskCategorySelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var taskCategories = _taskCategoryRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Name,
                Value = x.Id.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.TaskCategory, taskCategories);
        }

        private void GetTaskSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var tasks = _taskRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Name,
                Value = x.Id.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.Task, tasks);
        }

        private void GetSowRoleSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var sowRoles = _sowRoleRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Description,
                Value = x.Id.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.SOWRole, sowRoles);
        }

        private void GetSowSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var sows = _sowRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Name,
                Value = x.Id.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.SOW, sows);
        }

        private void GetSkillSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var skills = _skillRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Name,
                Value = x.Id.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.Skill, skills);
        }

        private void GetResourceSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var resources = _resourceRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = $"{x.FirstName} {x.LastName}",
                Value = x.Id.ToString()
            });
            formSelectorsDto.Add(FormSelectorConsts.Resource, resources);

        }

        private void GetProjectSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var projects = _projectRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Name,
                Value = x.Id.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.Project, projects);
        }

        private void GetLeaveTypeSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var leaveTypes = _leaveTypeRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Name,
                Value = x.Id.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.LeaveType, leaveTypes);
        }

        private void GetInvoiceInfoSelector(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var invoiceInfos = _invoiceInfoRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.InvoiceName,
                Value = x.Id.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.InvoiceInfo, invoiceInfos);
        }

        private void GetHolidaySelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var holidays = _holidayRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.HolidayName,
                Value = x.Id.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.Holiday, holidays);
        }

        private void GetDepartmentSelector(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var departments = _departmentRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Name,
                Value = x.Id.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.Department, departments);
        }

        private void GetClientSelectors(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var clients = _clientRepository.GetAllList().Select(x => new KeyValueDto
            {
                Name = x.Name,
                Value = x.Id.ToString()
            });

            formSelectorsDto.Add(FormSelectorConsts.Client, clients);
        }

        private void GetBeneficiariesSelector(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var beneficiaries = _beneficiaryRepository.GetAllList()
                .Select(x => new KeyValueDto
                {
                    Name = x.Name,
                    Value = x.Id.ToString()
                });

            formSelectorsDto.Add(FormSelectorConsts.Beneficiary, beneficiaries);
        }

        private void GetAllocationSelector(Dictionary<string, IEnumerable<KeyValueDto>> formSelectorsDto)
        {
            var allocations = _allocationRepository.GetAllList(x => x.IsActive)
                .Select(x => new KeyValueDto
                {
                    Name = x.Description,
                    Value = x.Id.ToString()
                });

            formSelectorsDto.Add(FormSelectorConsts.Allocation, allocations);
        }
    }
}
