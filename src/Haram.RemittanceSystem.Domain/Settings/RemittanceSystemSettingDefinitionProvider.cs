using Volo.Abp.Settings;

namespace Haram.RemittanceSystem.Settings;

public class RemittanceSystemSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(RemittanceSystemSettings.MySetting1));
    }
}
