using System.Threading.Tasks;
using Abp.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ET.ImportData
{
    public interface IImportDataBaseAppService<TEntity, TPrimaryKey, TImportDataDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TImportDataDto : class
    {
        Task<object> ImportDataAsync(IFormFile file);
    }
}
