using Haram.RemittanceSystem.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Haram.RemittanceSystem.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(RemittanceSystemEntityFrameworkCoreModule),
    typeof(RemittanceSystemApplicationContractsModule)
    )]
public class RemittanceSystemDbMigratorModule : AbpModule
{
}
