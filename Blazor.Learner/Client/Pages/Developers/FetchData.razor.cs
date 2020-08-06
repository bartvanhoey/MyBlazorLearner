using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Learner.Client.Pages.Developers
{
    public class FetchDataBase : ComponentBase
    {
        [Inject] public HttpClient HttpClient { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }

        protected Developer[] developers { get; set; }

        protected override async Task OnInitializedAsync()
        {
             developers = await HttpClient.GetFromJsonAsync<Developer[]>("api/developer");
        }

        protected async Task DeleteAsync(int developerId)
        {
            var developer = developers.FirstOrDefault(x => x.Id == developerId);
            if (await JSRuntime.InvokeAsync<bool>("confirm", $"Do you want to delete {developer.FirstName}"))
            {
                await HttpClient.DeleteAsync($"api/developer/{developerId}");
                await OnInitializedAsync();
            }
        }

    }
}