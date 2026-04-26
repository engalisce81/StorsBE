using Volo.Abp.Modularity;

namespace True.TECH;

/* Inherit from this class for your domain layer tests. */
public abstract class TECHDomainTestBase<TStartupModule> : TECHTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
