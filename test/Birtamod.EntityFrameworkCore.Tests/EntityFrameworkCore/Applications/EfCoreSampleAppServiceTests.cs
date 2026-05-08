using Birtamod.Samples;
using Xunit;

namespace Birtamod.EntityFrameworkCore.Applications;

[Collection(BirtamodTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<BirtamodEntityFrameworkCoreTestModule>
{

}
