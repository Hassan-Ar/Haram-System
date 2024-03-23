using Volo.Abp.Modularity;

namespace Haram.RemittanceSystem;

public abstract class RemittanceSystemApplicationTestBase<TStartupModule> : RemittanceSystemTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
