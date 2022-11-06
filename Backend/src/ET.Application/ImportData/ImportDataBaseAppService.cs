using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Npoi.Mapper;
using Npoi.Mapper.Attributes;
using NPOI.SS.UserModel;

namespace ET.ImportData
{
    public abstract class ImportDataBaseAppService<TEntity, TPrimaryKey, TImportDataDto> : ApplicationService, IImportDataBaseAppService<TEntity, TPrimaryKey, TImportDataDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TImportDataDto : class
    {
        protected readonly IRepository<TEntity, TPrimaryKey> _repository;

        protected ImportDataBaseAppService(IRepository<TEntity, TPrimaryKey> repository)
        {
            _repository = repository;
        }

        public virtual IEnumerable<T> MappingData<T>(IFormFile file) where T : class
        {
            var mapper = new Mapper(file.OpenReadStream());
            var firstSheet = mapper.Workbook.GetSheetAt(0);
            var header = firstSheet.GetRow(0);
            return CheckMatchingType(header) ? mapper.Take<T>(firstSheet.SheetName).Select(x => x.Value) : new List<T>();
        }

        protected bool CheckMatchingType(IRow headerRow)
        {
            var properties = typeof(TImportDataDto).GetProperties().Select(x => ((ColumnAttribute)x.GetCustomAttribute(typeof(ColumnAttribute)))?.Name ?? x.Name);
            return headerRow.Cells.All(cellName => properties.Contains(cellName.ToString()));
        }

        public virtual Task<object> ImportDataAsync(IFormFile file)
        {
            var dataDtos = MappingData<TImportDataDto>(file);

            if (dataDtos == null || !dataDtos.Any())
            {
                return Task.FromResult<object>(new
                {
                    success = false,
                    error = "Please re-check file imported or data",
                    message = ""
                });
              
            }
            var currentEntities = _repository.GetAll();
            var importedRow = 0;
            foreach (var data in dataDtos)
            {
                if (IsNullData(data)) continue;

                var createInput = ObjectMapper.Map<TEntity>(data);
                var duplicate = CheckDuplicate(createInput, currentEntities);
                if (duplicate == null) {
                    _repository.InsertAsync(createInput);
                    importedRow++;
                }
            }
            return Task.FromResult<object>(new
            {
                success = true,
                error = "",
                message = $"{importedRow}/{dataDtos.Count()} record are imported successfully"
            });
            
        }

        private bool IsNullData(TImportDataDto dto)
        {
            return dto.GetType().GetProperties().All(pro =>
            {
                var data = pro.GetValue(dto);
                if (data is DateTime time && time == default) return true;
                return data == null;
            });
        }
        private TEntity CheckDuplicate(TEntity insertEntity, IEnumerable<TEntity> currentEntities)
        {

            foreach (var existEntity in currentEntities)
            {
                var exist = true;
                foreach (var prop in insertEntity.GetType().GetProperties())
                {
                    if (prop.Name.Equals("Id")) continue;
                    
                    var insertData = prop.GetValue(insertEntity, null);
                    var existData = prop.GetValue(existEntity, null);

                    if (insertData is IEnumerable<object>) continue;

                    if (insertData == null && existData != null) 
                        exist = false;
                    if (insertData != null && existData == null) 
                        exist = false;
                    if (insertData != null && !insertData.Equals(existData))
                        exist = false;
                }
                if (exist)
                    return existEntity;
            }
            return null;
        }
    }
}
