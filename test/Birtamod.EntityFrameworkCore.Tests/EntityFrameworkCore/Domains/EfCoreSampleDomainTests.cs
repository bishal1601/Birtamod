using Birtamod.Samples;
using Xunit;

namespace Birtamod.EntityFrameworkCore.Domains;

[Collection(BirtamodTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<BirtamodEntityFrameworkCoreTestModule>
{

}
