using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace  Blazor.Learner.Client.Pages.Developers
{
    public class FormBase : ComponentBase
    {
         [Parameter] public Developer Developer { get; set; }
         [Parameter] public string ButtonText { get; set; } = "Save";
         [Parameter] public EventCallback OnValidSubmit { get; set; }
    }
}