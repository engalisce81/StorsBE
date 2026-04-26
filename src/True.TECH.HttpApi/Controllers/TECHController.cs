using True.TECH.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace True.TECH.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TECHController : AbpControllerBase
{
    protected TECHController()
    {
        LocalizationResource = typeof(TECHResource);
    }
}
