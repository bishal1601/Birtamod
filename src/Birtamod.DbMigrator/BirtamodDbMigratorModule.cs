using Birtamod.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Birtamod.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BirtamodEntityFrameworkCoreModule),
    typeof(BirtamodApplicationContractsModule)
)]
public class BirtamodDbMigratorModule : AbpModule
{
}
