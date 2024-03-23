using Volo.Abp.Modularity;

namespace Haram.RemittanceSystem;

[DependsOn(
    typeof(RemittanceSystemDomainModule),
    typeof(RemittanceSystemTestBaseModule)
)]
public class RemittanceSystemDomainTestModule : AbpModule
{

}
