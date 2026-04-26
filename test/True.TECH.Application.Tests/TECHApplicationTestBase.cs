using Volo.Abp.Modularity;

namespace True.TECH;

public abstract class TECHApplicationTestBase<TStartupModule> : TECHTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
