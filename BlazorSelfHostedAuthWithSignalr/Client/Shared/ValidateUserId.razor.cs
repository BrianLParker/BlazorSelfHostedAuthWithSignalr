using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorSelfHostedAuthWithSignalr.Client.Extensions;
using BlazorSelfHostedAuthWithSignalr.Client.Models.UserIdValidationStates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorSelfHostedAuthWithSignalr.Client.Shared;

public partial class ValidateUserId : ComponentBase
{
    private UserIdValidationState userIdValidationState = UserIdValidationState.Unknown;

    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
    [Parameter] public Guid UserId { get; set; } = default!;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? Valid { get; set; }
    [Parameter] public RenderFragment? Unknown { get; set; }
    [Parameter] public RenderFragment? Invalid { get; set; }

    private RenderFragment? CurrentFragment => userIdValidationState switch
    {
        UserIdValidationState.Unknown => this.Unknown ?? this.DefaultUnknown,
        UserIdValidationState.Valid => this.Valid ?? ChildContent,
        _ => this.Invalid ?? this.DefaultInvalid
    };

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        await ValidateIdAsync();
    }

    private async Task ValidateIdAsync()
    {
        this.userIdValidationState = UserIdValidationState.Unknown;
        AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        ClaimsPrincipal userClaimsPrincipal = authState.User;
        if (userClaimsPrincipal.Identity!.IsAuthenticated)
        {
            string userIdString = userClaimsPrincipal.FindFirstValue(claimType: "sub")!;
            Guid claimsUserId = Guid.Parse(userIdString);
            Guid UrlUserId = this.UserId;
            this.userIdValidationState = claimsUserId == UrlUserId ?
                UserIdValidationState.Valid :
                UserIdValidationState.Invalid;
        }
        else
        {
            this.userIdValidationState = UserIdValidationState.Invalid;
        }

        StateHasChanged();
    }
}