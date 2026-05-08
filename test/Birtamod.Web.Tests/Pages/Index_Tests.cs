using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Birtamod.Pages;

[Collection(BirtamodTestConsts.CollectionDefinitionName)]
public class Index_Tests : BirtamodWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
