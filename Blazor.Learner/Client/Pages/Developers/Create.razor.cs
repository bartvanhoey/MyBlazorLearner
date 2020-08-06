using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Learner.Client.Pages.Developers
{
  public class CreateBase : ComponentBase
  {

    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public HttpClient HttpClient { get; set; }
    protected Developer Developer = new Developer();

    protected async Task CreateDeveloperAsync()
    {
        var response = await HttpClient.PostAsJsonAsync("api/developer", Developer);
        response.EnsureSuccessStatusCode();

        NavigationManager.NavigateTo("developer");


    }
  }
}