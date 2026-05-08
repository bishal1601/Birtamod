using System.Threading.Tasks;

namespace Birtamod.Data;

public interface IBirtamodDbSchemaMigrator
{
    Task MigrateAsync();
}
