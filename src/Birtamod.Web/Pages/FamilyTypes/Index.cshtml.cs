using Microsoft.AspNetCore.Authorization;
using Birtamod.Permissions;

namespace Birtamod.Web.Pages.FamilyTypes;

[Authorize(BirtamodPermissions.FamilyTypes.Default)]
public class IndexModel : BirtamodPageModel
{
}
