using Birtamod.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Birtamod.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BirtamodController : AbpControllerBase
{
    protected BirtamodController()
    {
        LocalizationResource = typeof(BirtamodResource);
    }
}
