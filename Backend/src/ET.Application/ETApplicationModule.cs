using System;
using System.Globalization;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ET.Authorization;
using ET.Entities;
using ET.ImportData.Dto;
using ET.SoW.Dto;

namespace ET
{
    [DependsOn(
        typeof(ETCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ETApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ETAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ETApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg =>
                {
                    cfg.AddMaps(thisAssembly);
                    cfg.CreateMap<ProjectTypeImportDto, Entities.ProjectType>()
                        .ForMember(u => u.PL, 
                            options => options.MapFrom(input => input.PL.Equals("Yes",StringComparison.InvariantCultureIgnoreCase)));
                    cfg.CreateMap<SOW, SowDto>()
                        .ForMember(u => u.Version,
                            options => options.MapFrom(input => input.Version.HasValue ? input.Version.Value.ToString(CultureInfo.InvariantCulture) : string.Empty));
                });
        }
    }
}
