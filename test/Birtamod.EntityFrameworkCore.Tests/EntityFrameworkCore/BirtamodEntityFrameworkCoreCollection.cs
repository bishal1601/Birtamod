using Xunit;

namespace Birtamod.EntityFrameworkCore;

[CollectionDefinition(BirtamodTestConsts.CollectionDefinitionName)]
public class BirtamodEntityFrameworkCoreCollection : ICollectionFixture<BirtamodEntityFrameworkCoreFixture>
{

}
