using System.Linq;
using System.Threading.Tasks;
using Blazor.Learner.Server.Models;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{

  [Route("api/[controller]/[action]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
      var user = await _userManager.FindByNameAsync(request.UserName);

      if (user == null) return BadRequest("User does not exist");
      var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
      if (!signInResult.Succeeded) return BadRequest("Invalid Password");
      await _signInManager.SignInAsync(user, request.RememberMe);
      return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
      var user = new ApplicationUser();
      user.UserName = request.UserName;
      var result = await _userManager.CreateAsync(user, request.Password);
      if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);
      return await Login(new LoginRequest { UserName = request.UserName, Password = request.Password });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return Ok();
    }

    [HttpGet]
    public CurrentUser CurrentUserInfo()
    {
      return new CurrentUser
      {
        IsAuthenticated = User.Identity.IsAuthenticated,
        UserName = User.Identity.Name,
        Claims = User.Claims
          .ToDictionary(c => c.Type, c => c.Value)
      };
    }


  }
}