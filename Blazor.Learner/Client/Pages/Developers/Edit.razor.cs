using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Learner.Client.Pages.Developers
{
    public class EditBase : ComponentBase    
    {

        [Inject] public HttpClient HttpClient { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Parameter] public int DeveloperId { get; set; }
        protected Developer Developer = new Developer();

        protected override async Task OnParametersSetAsync()
        {
            Developer = await HttpClient.GetFromJsonAsync<Developer>($"api/developer/{DeveloperId}");
        }

        protected async Task EditDeveloperAsync()
        {
            await HttpClient.PutAsJsonAsync("api/developer", Developer);
            await JSRuntime.InvokeVoidAsync("alert", $"Updated Successfully!");
            NavigationManager.NavigateTo("developer");
        }



    }
}