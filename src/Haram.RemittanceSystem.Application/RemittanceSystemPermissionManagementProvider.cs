using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Guids;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;

namespace Haram.RemittanceSystem.Permissions
{
    internal class RemittanceSystemPermissionManagementProvider : PermissionManagementProvider
    {
        public RemittanceSystemPermissionManagementProvider(IPermissionGrantRepository permissionGrantRepository, IGuidGenerator guidGenerator, ICurrentTenant currentTenant) : base(permissionGrantRepository, guidGenerator, currentTenant)
        {
        }

        public override string Name => "RemittanceSystem";

 
    }
}
