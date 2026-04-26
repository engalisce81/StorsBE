using System.Threading.Tasks;

namespace True.TECH.Data;

public interface ITECHDbSchemaMigrator
{
    Task MigrateAsync();
}
