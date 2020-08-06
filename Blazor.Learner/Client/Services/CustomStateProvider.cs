using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blazor.Learner.Client.Services
{
  public class CustomAuthenticationStateProvider : AuthenticationStateProvider
  {
    private CurrentUser _currentUser;
    private readonly IAuthService _authService;

    public CustomAuthenticationStateProvider(IAuthService authService)
    {
      _authService = authService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
      var identity = new ClaimsIdentity();
      try
      {
        var currentUser = await GetCurrentUserAsync();
        if (currentUser.IsAuthenticated)
        {
          var claims = new[] { new Claim(ClaimTypes.Name, _currentUser.UserName) }
                .Concat(_currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));
          identity = new ClaimsIdentity(claims, "Server authentication");
        }
      }
      catch (HttpRequestException ex)
      {
        System.Console.WriteLine("Request failed: " + ex.ToString());
      }
      return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    private async Task<CurrentUser> GetCurrentUserAsync()
    {
      if (_currentUser != null && _currentUser.IsAuthenticated) return _currentUser;
      _currentUser = await _authService.GetCurrentUser();
      return _currentUser;
    }

    public async Task Logout()
    {
      await _authService.Logout();
      _currentUser = null;
      NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task Login(LoginRequest loginRequest)
    {
      await _authService.Login(loginRequest);
      NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task Register(RegisterRequest registerRequest)
    {
      await _authService.Register(registerRequest);
      NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

  }
}