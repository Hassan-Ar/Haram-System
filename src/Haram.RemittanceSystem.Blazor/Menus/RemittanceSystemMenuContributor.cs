﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Haram.RemittanceSystem.Localization;
using Haram.RemittanceSystem.MultiTenancy;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace Haram.RemittanceSystem.Blazor.Menus;

public class RemittanceSystemMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public RemittanceSystemMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<RemittanceSystemResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                RemittanceSystemMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home"
            )
        );
        context.Menu.AddItem(
    new ApplicationMenuItem(
        "Remittances",
        l["Remittances"],
        icon: "fa fa-wallet"
    ).AddItem(
        new ApplicationMenuItem(
            "Remittances.Remittances",
            l["RemittancesList"],
            url: "/remittances"
        )
    ).AddItem(
        new ApplicationMenuItem(
            "Remittances.Remittances",
            l["IssuedRemittances"],
            url: "/issuedremittances"
        )
    )
    .AddItem(
        new ApplicationMenuItem(
            "Remittances.ReadyRemittances",
            l["ReadyRemittances"],
            url: "/readyremittances"
        )
    )
     .AddItem(
        new ApplicationMenuItem(
            "Remittances.ApprovedRemittances",
            l["ApprovedRemittances"],
            url: "/approvedremittances"
        )
    )
     .AddItem(
        new ApplicationMenuItem(
            "Remittances.ReleasedRemittances",
            l["ReleasedRemittances"],
            url: "/releasedremittances"
        )
    )
);
        context.Menu.Items.Insert(
      0,
      new ApplicationMenuItem(
          RemittanceSystemMenus.Home,
          l["Customers"],
          "/customers",
          icon: "fas fa-users"
      )
  );
        context.Menu.Items.Insert(
0,
new ApplicationMenuItem(
    RemittanceSystemMenus.Home,
    l["Currencies"],
    "/currencies",
    icon: "fas fa-building"
)
);


        var administration = context.Menu.GetAdministration();

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        return Task.CompletedTask;
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();

        var authServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["MyAccount"],
            $"{authServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null).RequireAuthenticated());





        return Task.CompletedTask;
    }
}
