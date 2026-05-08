using Microsoft.AspNetCore.Builder;
using Birtamod;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("Birtamod.Web.csproj"); 
await builder.RunAbpModuleAsync<BirtamodWebTestModule>(applicationName: "Birtamod.Web");

public partial class Program
{
}
