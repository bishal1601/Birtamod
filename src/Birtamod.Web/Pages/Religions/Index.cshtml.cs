using Microsoft.AspNetCore.Authorization;
using Birtamod.Permissions;

namespace Birtamod.Web.Pages.Religions;

[Authorize(BirtamodPermissions.Religions.Default)]
public class IndexModel : BirtamodPageModel
{
}
