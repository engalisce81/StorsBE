using True.TECH.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace True.TECH.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TECHEntityFrameworkCoreModule),
    typeof(TECHApplicationContractsModule)
    )]
public class TECHDbMigratorModule : AbpModule
{
}
