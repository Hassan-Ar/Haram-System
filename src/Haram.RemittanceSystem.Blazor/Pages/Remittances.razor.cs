using Blazorise;
using Blazorise.DataGrid;
using Castle.Core.Logging;
using Haram.RemittanceSystem.Currencies;
using Haram.RemittanceSystem.Customers;
using Haram.RemittanceSystem.Permissions;
using Haram.RemittanceSystem.Remittances;
using Haram.RemittanceSystem.RemittanceTypes;
using Haram.RemittanceSystem.StatusTypes;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using static System.Reflection.Metadata.BlobBuilder;

namespace Haram.RemittanceSystem.Blazor.Pages
{
    public  partial class Remittances
    {
        public ICollection<CurrencyDto> Currencies { get; set; }
        public ICollection<CustomerDto> Customers { get; set; }
        //private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        //private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        public RemittanceType type { get; set; }
        public RemittanceType selectedtype { get; set; }

        public StatusType statusType { get; set; }
        public bool IsActiveStatusTypeFilter { get; set; }
        public bool IsActiveTypeFilter { get; set; }


        public GetListRemittancesInputDto inputGetList { get; set; } = new GetListRemittancesInputDto();
        public Guid customerID { get; set; }

        // public StatusTypes.StatusType? statusType { get; set; } 
        public Remittances()
        {
                // Constructor
                CreatePolicyName = RemittanceSystemPermissions.Remittances.Create;
                UpdatePolicyName = RemittanceSystemPermissions.Remittances.Edit;
                DeletePolicyName = RemittanceSystemPermissions.Remittances.Delete;
        }
            
        protected override async Task OnInitializedAsync()
        {
            Currencies = (await currencyappservice.GetListAsync(new PagedAndSortedResultRequestDto())).Items.ToList();
            Customers = (await customerappservice.GetListAsync(new PagedAndSortedResultRequestDto())).Items.ToList();
            CreatePolicyName = RemittanceSystemPermissions.Remittances.Create;
            UpdatePolicyName = RemittanceSystemPermissions.Remittances.Edit;
            DeletePolicyName = RemittanceSystemPermissions.Remittances.Delete;
        }
        public async Task applyFilter()
        {
            //if (IsActiveStatusTypeFilter && IsActiveTypeFilter)
            //{
            //    inputGetList = new GetListRemittancesInputDto();
            //}
            inputGetList.Status = statusType;
            inputGetList.Type = type;
            await GetRemittencAsync(Type: inputGetList.Type, Status: inputGetList.Status);
            await InvokeAsync(StateHasChanged);
            
        }
        private async Task GetRemittencAsync(RemittanceType? Type = null, StatusType? Status = null)
        {
           GetListInput.Status = Status;
            GetListInput.Type = Type;
            var result = await AppService.GetListAsync(
                GetListInput
                );
            Entities = result.Items;
            TotalCount = (int)result.TotalCount;
        }
        protected override async Task OnDataGridReadAsync(DataGridReadDataEventArgs<RemittanceDto> e)
        {
            //_logger.LogInformation("On Data Grid executed");
            await GetRemittencAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task SetAsReady(RemittanceDto remittanceDto)
        {
            await AppService.ChangeStatus(remittanceDto.Id,Guid.Empty);
            await GetRemittencAsync();
            await InvokeAsync(StateHasChanged);
        }

        public void set()
        {

        }
        Task OnSelectedTypeChanged(RemittanceType value)
        {
            selectedtype = value;
            if(value == RemittanceType.Internal)
            {

            }
            else
            {

            }
            return Task.CompletedTask;
        }
        Task OnSelectedCurrencyChanged(Guid value)
        {
            var curr = Currencies.Where(x=>x.Id == value).FirstOrDefault();
            if (curr.AlphabeticCode == "SYP")
            {
                
            }
            else
            {

            }
            return Task.CompletedTask;
        }
    }
}
