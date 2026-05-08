using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Birtamod.Data;
using Volo.Abp.DependencyInjection;

namespace Birtamod.EntityFrameworkCore;

public class EntityFrameworkCoreBirtamodDbSchemaMigrator
    : IBirtamodDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreBirtamodDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the BirtamodDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<BirtamodDbContext>()
            .Database
            .MigrateAsync();
    }
}
