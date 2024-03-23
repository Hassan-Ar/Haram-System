using System.Threading.Tasks;

namespace Haram.RemittanceSystem.Data;

public interface IRemittanceSystemDbSchemaMigrator
{
    Task MigrateAsync();
}
