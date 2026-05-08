using Microsoft.AspNetCore.Authorization;
using Birtamod.Permissions;

namespace Birtamod.Web.Pages.Languages;

[Authorize(BirtamodPermissions.Languages.Default)]
public class IndexModel : BirtamodPageModel
{
}
