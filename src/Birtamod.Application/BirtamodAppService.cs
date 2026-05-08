using Birtamod.Localization;
using Volo.Abp.Application.Services;

namespace Birtamod;

/* Inherit your application services from this class.
 */
public abstract class BirtamodAppService : ApplicationService
{
    protected BirtamodAppService()
    {
        LocalizationResource = typeof(BirtamodResource);
    }
}
