using Birtamod.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Birtamod.Web.Pages;

public abstract class BirtamodPageModel : AbpPageModel
{
    protected BirtamodPageModel()
    {
        LocalizationResourceType = typeof(BirtamodResource);
    }
}
