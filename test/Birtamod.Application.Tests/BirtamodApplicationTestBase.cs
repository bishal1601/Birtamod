using Volo.Abp.Modularity;

namespace Birtamod;

public abstract class BirtamodApplicationTestBase<TStartupModule> : BirtamodTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
