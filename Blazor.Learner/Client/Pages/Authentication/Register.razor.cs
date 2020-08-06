using System.Threading.Tasks;
using Blazor.Learner.Client.Services;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Learner.Client.Pages.Authentication
{
  public class RegisterBase : ComponentBase
  {
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public CustomAuthenticationStateProvider CustomStateProvider { get; set; }
    protected RegisterRequest RegisterRequest { get; set; } = new RegisterRequest();
    protected string error { get; set; }

    protected async Task HandleValidSubmitAsync()
    {
      error = null;
      try
      {
        await CustomStateProvider.Register(RegisterRequest);
        NavigationManager.NavigateTo("");
      }
      catch (System.Exception ex)
      {
        error = ex.Message;
      }
    }

  }
}