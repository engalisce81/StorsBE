using Microsoft.Extensions.Localization;
using True.TECH.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace True.TECH;

[Dependency(ReplaceServices = true)]
public class TECHBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<TECHResource> _localizer;

    public TECHBrandingProvider(IStringLocalizer<TECHResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
