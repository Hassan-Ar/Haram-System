using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Haram.RemittanceSystem.Data;

/* This is used if database provider does't define
 * IRemittanceSystemDbSchemaMigrator implementation.
 */
public class NullRemittanceSystemDbSchemaMigrator : IRemittanceSystemDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
