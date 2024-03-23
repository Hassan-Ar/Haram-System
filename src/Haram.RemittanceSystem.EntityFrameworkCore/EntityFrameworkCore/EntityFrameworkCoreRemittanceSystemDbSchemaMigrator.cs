using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Haram.RemittanceSystem.Data;
using Volo.Abp.DependencyInjection;

namespace Haram.RemittanceSystem.EntityFrameworkCore;

public class EntityFrameworkCoreRemittanceSystemDbSchemaMigrator
    : IRemittanceSystemDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreRemittanceSystemDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the RemittanceSystemDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<RemittanceSystemDbContext>()
            .Database
            .MigrateAsync();
    }
}
