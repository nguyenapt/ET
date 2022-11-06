using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using ET.Authorization;
using ET.Email.Dto;
using ET.Email.Helper;
using ET.Entities;
using ET.InternalTypes;
using ET.Shared;
using ET.Shared.Dto;
using ET.SoW.Dto;
using ET.SoW.ExportDtos;
using ET.SOWRoles;
using ET.SOWRoles.Dto;
using ET.Users;
using ET.Users.Dto;
using Task = System.Threading.Tasks.Task;

namespace ET.SoW
{
    [AbpAuthorize(PermissionNames.Pages_SoWs)]
    public class SowAppService : AsyncCrudAppService<SOW, SowDto, Guid, SowResultRequestDto, CreateSowDto, UpdateSowDto>, ISowAppService
    {
        private readonly IDataExportAppService _dataExportAppService;
        private readonly ISOWRoleAppService _roleAppService;
        private readonly IUserAppService _userAppService;
        private readonly IETEmailHelper _emailHelper;
        private readonly InternalTypeRepository _internalTypeRepository;

        public SowAppService(
            IRepository<SOW, Guid> repository,
            IDataExportAppService dataExportAppService,
            ISOWRoleAppService roleAppService,
            IUserAppService userAppService,
            IETEmailHelper emailHelper,
            InternalTypeRepository internalTypeRepository) : base(repository)
        {
            _dataExportAppService = dataExportAppService;
            _roleAppService = roleAppService;
            _userAppService = userAppService;
            _emailHelper = emailHelper;

            LocalizationSourceName = ETConsts.LocalizationSourceName;
            _internalTypeRepository = internalTypeRepository;
        }

        public override async Task<SowDto> UpdateAsync(UpdateSowDto input)
        {
            var sow = GetEntityByIdAsync(input.Id)?.Result;
            if (sow == null)
            {
                return await Task.FromException<SowDto>(new Exception("Please check sow id"));
            }

            var sendChangedSowNote = false;
            var sowStatusNote = sow.SowStatusNotes.FirstOrDefault(x => input.Id == x.SowId && input.Status == x.Status);

            if (input.StatusNote != sowStatusNote?.StatusNote)
            {
                if (sowStatusNote == null)
                {
                    sow.SowStatusNotes.Add(new SowStatusNote
                    {
                        SowId = input.Id,
                        Status = input.Status,
                        StatusNote = input.StatusNote
                    });
                }
                else
                {
                    sowStatusNote.StatusNote = input.StatusNote;
                }
                sendChangedSowNote = true;
            }

            var updatedSow = await base.UpdateAsync(input);

            if (sendChangedSowNote)
                NotifyChangedSowNote(updatedSow);

            return updatedSow;
        }
        public async Task<FileResultDto> GetSoWExportDataAsync(Guid id)
        {
            var sow = await GetAsync(new EntityDto<Guid>(id));
            var dataInput = new DataExportInput<SoWExportHeader, SoWExportItem, SoWExportFooter>
            {
                Header = new SoWExportHeader
                {
                    ClientName = sow.Project?.Client?.Name,
                    Department = sow.Project?.Department?.Name,
                    Project = sow.Project?.Name,
                    SoWName = sow.Name,
                    FileUrl = sow.FileUrl,
                    StartDate = sow.StartDate.HasValue ? sow.StartDate.Value.ToString("dd/MM/yyyy") : "N/A",
                    EndDate = sow.EndDate.HasValue ? sow.EndDate.Value.ToString("dd/MM/yyyy") : "N/A",
                    Status = sow.Status,
                    ClientPONumber = sow.ClientPONumber,
                    SOWNote = sow.Description,
                    SowNumber = sow.SowNumber.ToString(),
                    Version = sow.Version.ToString(CultureInfo.InvariantCulture)
                }
            };

            var dataResult = await _roleAppService.GetSoWRoleDetailList(id);
            dataInput.Items = dataResult.Items.Select(x =>
            {
                var item = ObjectMapper.Map<SoWExportItem>(x);
                item.IsBillable = x.IsBillable ? "Yes" : "No";
                item.FixRateCardFeeKV = ConvertToKeyValueList(x.FixRateCardFee);
                item.FixForecastFeeKV = ConvertToKeyValueList(x.FixForcastFee);
                item.MonthlyRateCardFeeKV = ConvertToKeyValueList(x.MonthlyRateCardFee);
                item.MonthlyForecastFeeKV = ConvertToKeyValueList(x.MonthlyForcastFee);

                return item;
            }).ToList();

            // Footer
            dataInput.Footer = new SoWExportFooter
            {
                TotalText = "Total",
                StartCellIndex = 11,
                FixRateCardFeeKV = ConvertToKeyValueList(dataResult.Totals.FixRateCardFee),
                FixForecastFeeKV = ConvertToKeyValueList(dataResult.Totals.FixForcastFee),
                MonthlyRateCardFeeKV = ConvertToKeyValueList(dataResult.Totals.MonthlyRateCardFee),
                MonthlyFixForecastFeeKV = ConvertToKeyValueList(dataResult.Totals.MonthlyForcastFee)
            };

            var byteData = _dataExportAppService.GetXlsExportData(dataInput);

            return new FileResultDto
            {
                Data = byteData,
                FileName = $"SoW_{sow.Name}_{sow.SowNumber}_{sow.Version}.xlsx"
            };
        }
        public Task<SowDto> CreateNewVersion(Guid parentId)
        {
            var sowParent = Repository
                .GetAllIncluding(x => x.Project, x => x.Project.Client, x => x.Project.Department, x => x.Beneficiary, x => x.SOWRoles)
                .FirstOrDefault(x => x.Id == parentId);

            if (sowParent != null)
            {
                var newSow = ObjectMapper.Map<CreateSowDto>(sowParent);
                newSow.Status = AppEnums.SowStatus.Draft.ToString();
                newSow.Name = $"{DateTime.UtcNow} {sowParent.Name}";
                return CreateAsync(newSow);
            }

            return Task.FromException<SowDto>(new Exception("Please recheck sow id"));
        }

        public override Task DeleteAsync(EntityDto<Guid> input)
        {
            var sow = Repository.GetAsync(input.Id).Result;
            if (sow != null && sow.Status.Equals(AppEnums.SowStatus.Draft.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return base.DeleteAsync(input);
            }
            return Task.FromException(new Exception(L("CanNotDeleteSow")));
        }

        protected override Task<SOW> GetEntityByIdAsync(Guid id)
        {
            var entity = GetSOWById(id);
            if (entity != null) entity.SOWRoles = entity.SOWRoles.OrderBy(x => x.CreationTime).ToList();
            return Task.FromResult(entity);
        }

        public async Task<SowDto> GetSowByIdWithoutSupporterRolesAsync(EntityDto<Guid> input)
        {
            var entity = base.GetAsync(input);
            if (entity?.Result?.SOWRoles != null)
            {
                foreach (var sowRole in entity.Result?.SOWRoles.ToList())
                {
                    var isInternalTypeAvailable = await _internalTypeRepository.IsInternalTypeSupporter
                        (new IsInternalTypeSupporterRequestDto(sowRole.InternalTypeId));
                    if (isInternalTypeAvailable)
                    {
                        entity.Result.SOWRoles.Remove(sowRole);
                    }
                }
            }
            return entity?.Result;
        }

        protected override IQueryable<SOW> CreateFilteredQuery(SowResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Project, x => x.Project.Department, x => x.Project.Client)
                .WhereIf(input.ClientId.HasValue, x => x.Project.ClientId == input.ClientId.Value)
                .WhereIf(input.DepartmentId.HasValue, x => x.Project.DepartmentId == input.DepartmentId.Value)
                .WhereIf(input.ProjectId.HasValue, x => x.ProjectId == input.ProjectId.Value)
                .WhereIf(input.StartDate.HasValue, x => x.StartDate >= input.StartDate.Value)
                .WhereIf(input.EndDate.HasValue, x => x.EndDate <= input.EndDate.Value)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword))
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
                .WhereIf(!input.ProjectTag.IsNullOrWhiteSpace(), x => x.Project.ProjectTag.Contains(input.ProjectTag))
                .WhereIf(input.SowNumber.HasValue, x => x.SowNumber == input.SowNumber)
                .WhereIf(input.Version.HasValue, x => x.Version == input.Version)
                .WhereIf(!input.Status.IsNullOrWhiteSpace(), x => input.Status.Contains(x.Status))
                .WhereIf(!input.ProjectCode.IsNullOrWhiteSpace(), x => x.Project.ProjectCode.Contains(input.ProjectCode));
        }
        protected override IQueryable<SOW> ApplySorting(IQueryable<SOW> query, SowResultRequestDto input)
        {
            if (!string.IsNullOrEmpty(input.SortBy))
            {
                var props = input.SortBy.Split(',');
                if (props.Length == 2)
                {
                    switch (props[0].ToLower())
                    {
                        case "client":
                            if (props[1].Equals("asc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderBy(x => x.Project.Client.Name);
                            if (props[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderByDescending(x => x.Project.Client.Name);
                            break;
                        case "division":
                            if (props[1].Equals("asc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderBy(x => x.Project.Department.Name);
                            if (props[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderByDescending(x => x.Project.Department.Name);
                            break;
                        case "project":
                            if (props[1].Equals("asc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderBy(x => x.Project.Name);
                            if (props[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderByDescending(x => x.Project.Name);
                            break;
                        case "sowname":
                            if (props[1].Equals("asc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderBy(x => x.Name);
                            if (props[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderByDescending(x => x.Name);
                            break;
                        case "status":
                            if (props[1].Equals("asc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderBy(x => x.Status);
                            if (props[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderByDescending(x => x.Status);
                            break;
                        case "startdate":
                            if (props[1].Equals("asc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderBy(x => x.StartDate);
                            if (props[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderByDescending(x => x.StartDate);
                            break;
                        case "enddate":
                            if (props[1].Equals("asc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderBy(x => x.EndDate);
                            if (props[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderByDescending(x => x.EndDate);
                            break;
                        case "creationtime":
                            if (props[1].Equals("asc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderBy(x => x.CreationTime);
                            if (props[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderByDescending(x => x.CreationTime);
                            break;
                        case "sownumber":
                            if (props[1].Equals("asc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderBy(x => x.SowNumber);
                            if (props[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase)) return query.OrderByDescending(x => x.SowNumber);
                            break;
                        default:
                            return query.OrderByDescending(x => x.CreationTime);
                    }
                }
            }

            return query.OrderByDescending(x => x.CreationTime);
        }
        protected override IQueryable<SOW> ApplyPaging(IQueryable<SOW> query, SowResultRequestDto input)
        {
            if (input.PageSize <= 0)
            {
                var totalCount = AsyncQueryableExecuter.CountAsync(query).Result;
                input.PageSize = totalCount == 0 ? totalCount + 1 : totalCount;
            }

            if (input.CurrentPage == 0) input.CurrentPage = 1;
            return query.Skip((input.CurrentPage - 1) * input.PageSize).Take(input.PageSize);
        }
        private List<IKeyValue> ConvertToKeyValueList(IEnumerable<SoWRoleFeeDto> items)
        {
            return items.Select(x =>
            {
                IKeyValue kv = new KeyDecimalValue { Name = x.Currency, Value = x.Fee };
                return kv;
            }).ToList();
        }

        private void NotifyChangedSowNote(SowDto updatedSowResult)
        {
            if (updatedSowResult.Project == null || updatedSowResult.Project.PMOResource == null)
                return;

            var editingUser = new UserDto();
            if (updatedSowResult.LastModifierUserId != null)
                editingUser = _userAppService.GetAsync(new EntityDto<long>(updatedSowResult.LastModifierUserId.Value)).Result;


            var changedSowStatusEmailDto = new ChangedSowStatusEmailDto
            {
                FullName = $"{updatedSowResult.Project.PMOResource.FirstName} {updatedSowResult.Project.PMOResource.LastName}",
                SoWNumber = updatedSowResult.SowNumber?.ToString(),
                LastEditorEmail = editingUser.EmailAddress,
                SoWName = updatedSowResult.Name,
                SoWVersion = updatedSowResult.Version,
                StatusNote = updatedSowResult.SowStatusNotes.FirstOrDefault(x => x.Status == updatedSowResult.Status)?.StatusNote
            };

            _emailHelper.SendMailGenericAsync(ETConsts.ETEmailSettings.SowNoteChangedName,
                new List<string> { $"{updatedSowResult.Project.PMOResource.User.EmailAddress}" },
                null,
                null,
                changedSowStatusEmailDto,
                null);

        }

        private SOW GetSOWById(Guid id)
        {
            return Repository
                .GetAllIncluding(
                    x => x.Project,
                    x => x.Project.Client,
                    x => x.Project.Department,
                    x => x.Project.PMOResource.User,
                    x => x.Project.Client.KAMResource,
                    x => x.Project.Client.PMOResource,
                    x => x.Beneficiary,
                    x => x.SOWRoles,
                    x => x.SowStatusNotes)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}

