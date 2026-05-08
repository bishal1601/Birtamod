using Microsoft.AspNetCore.Authorization;
using Birtamod.Permissions;

namespace Birtamod.Web.Pages.Citizens;

[Authorize(BirtamodPermissions.Citizens.Default)]
public class IndexModel : BirtamodPageModel
{
}
