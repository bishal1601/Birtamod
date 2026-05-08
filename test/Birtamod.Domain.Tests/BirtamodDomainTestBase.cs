using Volo.Abp.Modularity;

namespace Birtamod;

/* Inherit from this class for your domain layer tests. */
public abstract class BirtamodDomainTestBase<TStartupModule> : BirtamodTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
