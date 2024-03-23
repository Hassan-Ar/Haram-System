using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Haram.RemittanceSystem.Blazor;

[Dependency(ReplaceServices = true)]
public class RemittanceSystemBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "RemittanceSystem";
}
