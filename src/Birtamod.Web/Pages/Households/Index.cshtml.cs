using Microsoft.AspNetCore.Authorization;
using Birtamod.Permissions;

namespace Birtamod.Web.Pages.Households;

[Authorize(BirtamodPermissions.Households.Default)]
public class IndexModel : BirtamodPageModel
{
}
