using Volo.Abp.Modularity;

namespace Birtamod;

[DependsOn(
    typeof(BirtamodDomainModule),
    typeof(BirtamodTestBaseModule)
)]
public class BirtamodDomainTestModule : AbpModule
{

}
