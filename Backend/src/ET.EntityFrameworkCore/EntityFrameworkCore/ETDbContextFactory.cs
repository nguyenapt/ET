using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ET.Configuration;
using ET.Web;

namespace ET.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ETDbContextFactory : IDesignTimeDbContextFactory<ETDbContext>
    {
        public ETDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ETDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ETDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ETConsts.ConnectionStringName));

            return new ETDbContext(builder.Options);
        }
    }
}
