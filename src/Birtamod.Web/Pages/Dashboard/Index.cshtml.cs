using Microsoft.AspNetCore.Authorization;
using Birtamod.Permissions;

namespace Birtamod.Web.Pages.Dashboard;

[Authorize(BirtamodPermissions.Dashboard.View)]
public class IndexModel : BirtamodPageModel
{
}
