using Volo.Abp.Modularity;

namespace True.TECH;

[DependsOn(
    typeof(TECHApplicationModule),
    typeof(TECHDomainTestModule)
)]
public class TECHApplicationTestModule : AbpModule
{

}
