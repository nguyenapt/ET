using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ET.Authorization.Users;
using ET.Entities;
using ET.ImportData.Dto;
using Microsoft.AspNetCore.Http;

namespace ET.ImportData
{
    public class LeaveBankDataImport : ImportDataBaseAppService<LeaveBank, Guid, LeaveBankDto>
    {
        protected readonly IRepository<Resource, Guid> _resourceRepository;
        protected readonly IRepository<LeaveType, Guid> _leaveTypeRepository;
        protected readonly IRepository<User, long> _userRepository;
        public LeaveBankDataImport(IRepository<LeaveBank, Guid> repository, IRepository<Resource, Guid> resourceRepository, IRepository<LeaveType, Guid> leaveTypeRepository, IRepository<User, long> userRepository) : base(repository)
        {
            _resourceRepository = resourceRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _userRepository = userRepository;
        }

        public override Task<object> ImportDataAsync(IFormFile file)
        {
            var dataDtos = MappingData<LeaveBankDto>(file);
            if (dataDtos == null || !dataDtos.Any())
            {
                return System.Threading.Tasks.Task.FromResult<object>(new
                {
                    success = false,
                    error = "Please re-check file imported or data",
                    message = "Please re-check file imported or data"
                });
            }

            var currentLeaveBanks = _repository.GetAll();
            var updatedRow = 0;
            var newRow = 0;
            foreach (var dto in dataDtos)
            {
                var user = _userRepository.GetAll().AsEnumerable().FirstOrDefault(x => x.EmailAddress.Equals(dto.ResourceEmail, StringComparison.InvariantCultureIgnoreCase));
                if (user == null) continue;

                var resource = _resourceRepository.GetAll().FirstOrDefault(x => x.UserId == user.Id);
                if (resource != null)
                {
                    var leaveType = _leaveTypeRepository.GetAll().AsEnumerable().FirstOrDefault(x => x.Name.Equals(dto.LeaveTypeName, StringComparison.InvariantCultureIgnoreCase));
                    if (leaveType == null)
                    {
                        leaveType = _leaveTypeRepository.InsertAsync(new LeaveType() { Name = dto.LeaveTypeName, Description = dto.LeaveTypeName }).Result;
                    }
                    var exist = currentLeaveBanks.Any() ? currentLeaveBanks.FirstOrDefault(x => x.ResourceId == resource.Id && x.LeaveTypeId == leaveType.Id) : null;
                    if (exist != null)
                    {
                        exist.TotalAllowedHours = dto.TotalAllowedHours;
                        exist.Year = dto.Year;
                        updatedRow++;
                        _repository.UpdateAsync(exist);
                    }
                    else
                    {
                        var createInput = new LeaveBank
                        {
                            ResourceId = resource.Id,
                            LeaveTypeId = leaveType.Id,
                            Year = dto.Year,
                            TotalAllowedHours = dto.TotalAllowedHours
                        };
                        newRow++;
                        _repository.InsertAsync(createInput);
                    }
                }
            }

            return System.Threading.Tasks.Task.FromResult<object>(new
            {
                success = true,
                error = "",
                message = $"{newRow}/{dataDtos.Count()} leave banks are imported successfully " +
              $"and {updatedRow}/{dataDtos.Count()} leave banks are updated successfully"
            });
        }
    }
}
