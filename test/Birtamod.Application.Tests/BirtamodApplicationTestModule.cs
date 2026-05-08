using Volo.Abp.Modularity;

namespace Birtamod;

[DependsOn(
    typeof(BirtamodApplicationModule),
    typeof(BirtamodDomainTestModule)
)]
public class BirtamodApplicationTestModule : AbpModule
{

}
