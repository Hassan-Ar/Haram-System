using Blazorise;
using Blazorise.DataGrid;
using Castle.Core.Logging;
using Haram.RemittanceSystem.Currencies;
using Haram.RemittanceSystem.Customers;
using Haram.RemittanceSystem.Permissions;
using Haram.RemittanceSystem.Remittances;
using Haram.RemittanceSystem.RemittanceTypes;
using Haram.RemittanceSystem.StatusTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Haram.RemittanceSystem.Blazor.Pages
{
    public partial class Reports
    {
        public ICollection<CurrencyDto> Currencies { get; set; }
        public IEnumerable<CurrencyDto> CurrenciesList { get; set; }
        public string name { get; set; }
        public ICollection<CustomerDto> Customers { get; set; }
        private int TotalCount { get; set; }
        public RemittanceType type { get; set; }
        public RemittanceType selectedtype { get; set; }
        public StatusType statusType { get; set; }
        private ValidationMessageStore validationMessages;
        public GetListRemittancesInputDto inputGetList { get; set; } = new GetListRemittancesInputDto();
        public Guid customerID { get; set; }
        private bool CanCreateRemittance { get; set; }
        public bool searchBoxIsActive { get; set; } = true;
        public bool IsActiveCurrency { get; set; } = true;
        public bool IsActiveRemittanceType { get; set; } = false;

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        public DateTime? FirstDate { get; set; } 
        public DateTime? SecondDate { get; set; }

        // Defult CTOR 
        public Reports()
        {

            CreatePolicyName = RemittanceSystemPermissions.Remittances.Create;
            UpdatePolicyName = RemittanceSystemPermissions.Remittances.Edit;
            DeletePolicyName = RemittanceSystemPermissions.Remittances.Delete;

        }

        //override OnInitializedAsync to get Currencies , Customers Lists and set the permissions
        protected override async Task OnInitializedAsync()
        {
            Currencies = (await currencyappservice.GetListAsync(new PagedAndSortedResultRequestDto() { SkipCount = 0, MaxResultCount = 1000 })).Items.ToList();
            Customers = (await customerappservice.GetListAsync(new PagedAndSortedResultRequestDto())).Items.ToList();
            CurrenciesList = Currencies.Where(x => x.AlphabeticCode == "SYP").ToList();
            await SetPermissionsAsync();
        }

        // to set permissions
        protected override async Task SetPermissionsAsync()
        {
            CanCreateRemittance = await AuthorizationService
                .IsGrantedAsync(RemittanceSystemPermissions.Remittances.Create);

        }
        // to get all Types of remittance , and it can filter the customar name 
        private async Task GetRemittencAsync(RemittanceType? Type = null, StatusType? Status = null)
        {
            if (FirstDate != null && FirstDate != null )
            {
                GetListInput.FirstDate = FirstDate;
                GetListInput.SecondDate = SecondDate;
            }
            if (name != string.Empty)
            {
                GetListInput.name = name;
            }
            GetListInput.Status = Status;
            GetListInput.Type = Type;
            GetListInput.MaxResultCount = PageSize;
            GetListInput.SkipCount = CurrentPage * PageSize;
            GetListInput.Sorting = CurrentSorting;
            var result = await AppService.GetListAsync(
                GetListInput
                );
            Entities = result.Items;
            TotalCount = (int)result.TotalCount;
        }
        // overriding OnDataGridReadAsync to get all Types of remittance
        protected override async Task OnDataGridReadAsync(DataGridReadDataEventArgs<RemittanceDto> e)
        {
            CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");
            CurrentPage = e.Page - 1;
            await GetRemittencAsync();
            await InvokeAsync(StateHasChanged);
        }

        // apply constarints on remittance type and currency
        private async Task OnSelectType()
        {
            // Check  Remittance Type 
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

        // to apply filter on customer name with (name) property
        private async Task OnNameChange()
        {
            searchBoxIsActive = false;
            await GetRemittencAsync(Type: inputGetList.Type, Status: inputGetList.Status);
            await InvokeAsync(StateHasChanged);
        }
        // to clean the search box in th razor page
        private async Task SetSearchBoxEmpty()
        {
            searchBoxIsActive = true;
            name = null;
            await GetRemittencAsync(Type: inputGetList.Type, Status: inputGetList.Status);
            await InvokeAsync(StateHasChanged);

        }
        private async Task getRemittancesTowDates()
        {

            await GetRemittencAsync(Type: inputGetList.Type, Status: inputGetList.Status);
            await InvokeAsync(StateHasChanged);
        }
    }
}
