using System.Threading.Tasks;
using Blazor.Learner.Client.Services;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Learner.Client.Pages.Authentication
{
  public class LoginBase : ComponentBase
  {
    protected LoginRequest LoginRequest { get; set; } = new LoginRequest();
    protected string error { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public CustomAuthenticationStateProvider CustomStateProvider { get; set; }

    protected async Task HandleValidSubmitAsync()
    {
      try
      {
        await CustomStateProvider.Login(LoginRequest);
        NavigationManager.NavigateTo("");
      }
      catch (System.Exception ex)
      {
        error = ex.Message;
      }
    }
  }
}