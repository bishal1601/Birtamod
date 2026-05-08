using Volo.Abp.Settings;

namespace Birtamod.Settings;

public class BirtamodSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BirtamodSettings.MySetting1));
    }
}
