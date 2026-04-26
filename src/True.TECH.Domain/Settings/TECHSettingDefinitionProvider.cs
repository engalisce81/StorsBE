using Volo.Abp.Settings;

namespace True.TECH.Settings;

public class TECHSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(TECHSettings.MySetting1));
    }
}
