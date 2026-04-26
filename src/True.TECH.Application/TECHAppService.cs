using System;
using System.Collections.Generic;
using System.Text;
using True.TECH.Localization;
using Volo.Abp.Application.Services;

namespace True.TECH;

/* Inherit your application services from this class.
 */
public abstract class TECHAppService : ApplicationService
{
    protected TECHAppService()
    {
        LocalizationResource = typeof(TECHResource);
    }
}
