using Abp.Domain.Repositories;
using ET.ImportData.Dto;

namespace ET.ImportData
{
    public class ProjectTypeDataImport : ImportDataBaseAppService<Entities.ProjectType, int, ProjectTypeImportDto>
    {
        public ProjectTypeDataImport(IRepository<Entities.ProjectType, int> repository) : base(repository)
        {
        }
    }
}
