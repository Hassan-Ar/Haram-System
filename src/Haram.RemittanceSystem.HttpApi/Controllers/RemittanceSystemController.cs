using Haram.RemittanceSystem.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Haram.RemittanceSystem.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class RemittanceSystemController : AbpControllerBase
{
    protected RemittanceSystemController()
    {
        LocalizationResource = typeof(RemittanceSystemResource);
    }
}
