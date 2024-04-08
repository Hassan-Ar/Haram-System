using Blazorise;
using Blazorise.DataGrid;
using Haram.RemittanceSystem.Currencies;
using Haram.RemittanceSystem.Customers;
using Haram.RemittanceSystem.Permissions;
using Haram.RemittanceSystem.Remittances;
using Haram.RemittanceSystem.RemittanceTypes;
using Haram.RemittanceSystem.StatusTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Haram.RemittanceSystem.Blazor.Pages
{
    public partial class IssuedRemittances
    {
        public Modal? SetReadyModal { get; set; }
        public ICollection<CurrencyDto> Currencies { get; set; }
        public IEnumerable<CurrencyDto> CurrenciesList { get; set; }
        public ICollection<CustomerDto> Customers { get; set; }
        public RemittanceType type { get; set; }
        public RemittanceType selectedtype { get; set; }
        public StatusType statusType { get; set; }
        private int TotalCount { get; set; }
        public bool IsActiveCurrency { get; set; } = true;
        public bool IsActiveRemittanceType { get; set; } = false;
        public GetListRemittancesInputDto inputGetList { get; set; } = new GetListRemittancesInputDto();
        public RemittanceDto ReadyRemittance { get; set; } = new RemittanceDto() { Sender = new CustomerDto() { }, currency = new CurrencyDto() { } };
        public Guid customerID { get; set; }
        public bool CanEditRemittance { get; private set; }
        public bool CanDeleteRemittance { get; private set; }
        public bool CanSetReadyRemittance { get; private set; }


        private ValidationMessageStore validationMessages;


        // Defulte Constructor
        public IssuedRemittances()
        {

            CreatePolicyName = RemittanceSystemPermissions.Remittances.Create;
            UpdatePolicyName = RemittanceSystemPermissions.Remittances.Edit;
            DeletePolicyName = RemittanceSystemPermissions.Remittances.Delete;

        }

        // overriding OnInitializedAsync to get Currencies and Customers and get the Permissions
        protected override async Task OnInitializedAsync()
        {
            Currencies = (await currencyappservice.GetListAsync(new PagedAndSortedResultRequestDto() { SkipCount = 0, MaxResultCount = 1000 })).Items.ToList();
            Customers = (await customerappservice.GetListAsync(new PagedAndSortedResultRequestDto())).Items.ToList();
            CurrenciesList = Currencies.Where(x => x.AlphabeticCode == "SYP").ToList();
            await SetPermissionsAsync();

        }

        // get Issued Remittances
        private async Task GetRemittencAsync(RemittanceType? Type = null, StatusType? Status = null)
        {
            GetListInput.Status = StatusType.Issued;
            GetListInput.Type = Type;
            var result = await AppService.GetListAsync(
                GetListInput
                );
            Entities = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        // overriding OnDataGridReadAsync to get only issued remittances 
        protected override async Task OnDataGridReadAsync(DataGridReadDataEventArgs<RemittanceDto> e)
        {
            await GetRemittencAsync();
            await InvokeAsync(StateHasChanged);
        }

        // to set remittance ready
        private async Task SetAsReady(RemittanceDto remittanceDto)
        {
            await AppService.ChangeStatus(remittanceDto.Id, Guid.Empty);
            await GetRemittencAsync();
            await InvokeAsync(StateHasChanged);
        }

        // to apply constraint on the remittance type and the currency
        private async Task OnSelectType()
        {
            // Check currency type if its internal
            if (NewEntity.Type == RemittanceType.Internal)
            {
                IsActiveCurrency = true;
                var currency = Currencies.FirstOrDefault(x => x.AlphabeticCode == "SYP");
                NewEntity.CurrencyID = currency.Id;
                CurrenciesList = Currencies.Where(x => x.AlphabeticCode == "SYP").ToList();
            }
            else
            {
                IsActiveCurrency = false;
                CurrenciesList = Currencies.Where(x => x.AlphabeticCode != "SYP").ToList();
            }
        }
        // to set permissions
        protected override async Task SetPermissionsAsync()
        {
            CanEditRemittance = await AuthorizationService
                .IsGrantedAsync(RemittanceSystemPermissions.Remittances.Edit);
            CanDeleteRemittance = await AuthorizationService
                .IsGrantedAsync(RemittanceSystemPermissions.Remittances.Delete);
            CanSetReadyRemittance = await AuthorizationService
                .IsGrantedAsync(RemittanceSystemPermissions.Remittances.SetReady);
        }
        // to show Releasing Remittence Modal
        private Task ShowReleasingRemittanceModal()
        {
            return SetReadyModal.Show();
        }
        // to Hide SetReady Remittence Modal
        private Task HideSetReadyRemittanceModal()
        {
            return SetReadyModal.Hide();
        }

        // Set the status ready
        private async Task SetReady()
        {
            await AppService.ChangeStatus(ReadyRemittance.Id, Guid.Empty);
            await GetRemittencAsync();
            await InvokeAsync(StateHasChanged);
            InvokeAsync(SetReadyModal.Hide);

        }
        // to open the set ready modal and pass the entity
        protected virtual async Task OpenSetReadyModalAsync(RemittanceDto entity)
        {
            try
            {
                var entityDto = await AppService.GetAsync(entity.Id);
                ReadyRemittance.Id = entity.Id;
                ReadyRemittance = entity;

                await InvokeAsync(async () =>
                {
                    StateHasChanged();
                    if (SetReadyModal != null)
                    {
                        await SetReadyModal.Show();
                    }
                });
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
        protected override Task OnUpdatedEntityAsync()
        {
            if (EditingEntity.CurrencyID == null || EditingEntity.CurrencyID == Guid.Empty)
            {
                throw new UserFriendlyException(L["SelectCurrency"]);
            }
            if (EditingEntity.SenderId == null || EditingEntity.SenderId == Guid.Empty)
            {
                throw new UserFriendlyException(L["SelectSender"]);
            }
            return base.OnUpdatedEntityAsync();
        }

        //    private ConfirmationDialog confirmationDialog;

        //    private async Task ShowConfirmation()
        //    {
        //        confirmationDialog.ShowDialog();
        //        StateHasChanged();

        //    }

        //    private async Task HandleConfirmationChanged(bool result)
        //    {
        //        if (result)
        //        {
        //           await SetReady();
        //        }
        //        else
        //        {
        //            // Handle cancellation
        //            Console.WriteLine("Action canceled!");
        //        }
        //    }
        //}
    }
}

