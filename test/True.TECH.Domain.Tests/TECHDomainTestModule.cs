using Volo.Abp.Modularity;

namespace True.TECH;

[DependsOn(
    typeof(TECHDomainModule),
    typeof(TECHTestBaseModule)
)]
public class TECHDomainTestModule : AbpModule
{

}
