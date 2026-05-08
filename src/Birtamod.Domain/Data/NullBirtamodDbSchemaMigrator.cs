using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Birtamod.Data;

/* This is used if database provider does't define
 * IBirtamodDbSchemaMigrator implementation.
 */
public class NullBirtamodDbSchemaMigrator : IBirtamodDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
