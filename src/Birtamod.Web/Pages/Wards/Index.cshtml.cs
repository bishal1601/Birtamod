using Microsoft.AspNetCore.Authorization;
using Birtamod.Permissions;

namespace Birtamod.Web.Pages.Wards;

[Authorize(BirtamodPermissions.Wards.Default)]
public class IndexModel : BirtamodPageModel
{
}
