using Microsoft.AspNetCore.Authorization;
using Birtamod.Permissions;

namespace Birtamod.Web.Pages.Ethnicities;

[Authorize(BirtamodPermissions.Ethnicities.Default)]
public class IndexModel : BirtamodPageModel
{
}
