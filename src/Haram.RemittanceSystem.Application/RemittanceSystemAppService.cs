using System;
using System.Collections.Generic;
using System.Text;
using Haram.RemittanceSystem.Localization;
using Volo.Abp.Application.Services;

namespace Haram.RemittanceSystem;

/* Inherit your application services from this class.
 */
public abstract class RemittanceSystemAppService : ApplicationService
{
    protected RemittanceSystemAppService()
    {
        LocalizationResource = typeof(RemittanceSystemResource);
    }
}
