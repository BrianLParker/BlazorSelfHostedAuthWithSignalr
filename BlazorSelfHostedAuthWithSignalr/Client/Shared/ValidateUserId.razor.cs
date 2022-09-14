using System;
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
        userIdValidationState = UserIdValidationState.Unknown;
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        if (user.Identity!.IsAuthenticated)
        {
            string userIdString = user.FindFirstValue("sub")!;
            Guid claimsUserId = Guid.Parse(userIdString);
            Guid UrlUserId = this.UserId;
            if (claimsUserId == UrlUserId)
            {
                userIdValidationState = UserIdValidationState.Valid;
            }
            else
            {
                userIdValidationState = UserIdValidationState.Invalid;
            }
        }
        else
        {
            userIdValidationState = UserIdValidationState.Invalid;
        }

        StateHasChanged();
    }
}