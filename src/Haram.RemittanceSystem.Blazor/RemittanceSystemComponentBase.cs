using Haram.RemittanceSystem.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Haram.RemittanceSystem.Blazor;

public abstract class RemittanceSystemComponentBase : AbpComponentBase
{
    protected RemittanceSystemComponentBase()
    {
        LocalizationResource = typeof(RemittanceSystemResource);
    }
}
