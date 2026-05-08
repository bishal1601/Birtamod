using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using Birtamod.Localization;

namespace Birtamod.Web;

[Dependency(ReplaceServices = true)]
public class BirtamodBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<BirtamodResource> _localizer;

    public BirtamodBrandingProvider(IStringLocalizer<BirtamodResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
