using System.Threading.Tasks;
using Blazor.Learner.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blazor.Learner.Client.Shared
{
  public class MainLayoutBase : LayoutComponentBase
  {
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public CustomAuthenticationStateProvider CustomAuthenticationStateProvider { get; set; }
    [CascadingParameter] protected Task<AuthenticationState> AuthenticationState { get; set; }

    protected override async Task OnParametersSetAsync()
    {
      if (!(await AuthenticationState).User.Identity.IsAuthenticated)
      {
        NavigationManager.NavigateTo("/login");
      }
    }

    protected async Task LogoutClick()
    {
        await CustomAuthenticationStateProvider.Logout();
        NavigationManager.NavigateTo("/login");
    }

  }
}