using Volo.Abp.Modularity;

namespace Haram.RemittanceSystem;

/* Inherit from this class for your domain layer tests. */
public abstract class RemittanceSystemDomainTestBase<TStartupModule> : RemittanceSystemTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
