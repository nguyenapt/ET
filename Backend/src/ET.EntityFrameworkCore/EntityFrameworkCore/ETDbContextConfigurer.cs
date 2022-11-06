using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ET.EntityFrameworkCore
{
    public static class ETDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ETDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ETDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
