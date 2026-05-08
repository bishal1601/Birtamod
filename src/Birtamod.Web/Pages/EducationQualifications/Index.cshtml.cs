using Microsoft.AspNetCore.Authorization;
using Birtamod.Permissions;

namespace Birtamod.Web.Pages.EducationQualifications;

[Authorize(BirtamodPermissions.EducationQualifications.Default)]
public class IndexModel : BirtamodPageModel
{
}
