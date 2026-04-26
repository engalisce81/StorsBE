using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace True.TECH.Data;

/* This is used if database provider does't define
 * ITECHDbSchemaMigrator implementation.
 */
public class NullTECHDbSchemaMigrator : ITECHDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
