using Volo.Abp.Modularity;

namespace Haram.RemittanceSystem;

[DependsOn(
    typeof(RemittanceSystemApplicationModule),
    typeof(RemittanceSystemDomainTestModule)
)]
public class RemittanceSystemApplicationTestModule : AbpModule
{

}
