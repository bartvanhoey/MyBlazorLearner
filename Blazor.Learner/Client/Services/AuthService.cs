using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazor.Learner.Shared.Models;

namespace Blazor.Learner.Client.Services
{
  public class AuthService : IAuthService
  {
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<CurrentUser> GetCurrentUser()
    {
      var response = await _httpClient.GetFromJsonAsync<CurrentUser>("api/auth/currentuserinfo");
      return response;
    }

    public async Task Login(LoginRequest loginRequest)
    {
      var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);
      if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        throw new Exception(await response.Content.ReadAsStringAsync());
      response.EnsureSuccessStatusCode();
    }

    public async Task Logout()
    {
      var response = await _httpClient.PostAsync("api/auth/logout", null);
      response.EnsureSuccessStatusCode();
    }

    public async Task Register(RegisterRequest registerRequest)
    {
      var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerRequest);
      if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        throw new Exception(await response.Content.ReadAsStringAsync());
      response.EnsureSuccessStatusCode();
    }
  }
}