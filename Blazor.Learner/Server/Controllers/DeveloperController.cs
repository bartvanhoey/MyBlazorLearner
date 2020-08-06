using System.Threading.Tasks;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DeveloperController : ControllerBase
  {
    private readonly ApplicationDBContext _context;

    public DeveloperController(ApplicationDBContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      return Ok(await _context.Developers.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var dev = await _context.Developers.FirstOrDefaultAsync(a => a.Id == id);
      return Ok(dev);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Developer developer)
    {
      _context.Add(developer);
      await _context.SaveChangesAsync();
      return Ok(developer.Id);
    }

    [HttpPut]
    public async Task<IActionResult> Put(Developer developer)
    {
      _context.Entry(developer).State = EntityState.Modified;
      await _context.SaveChangesAsync();
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var dev = new Developer { Id = id };
      _context.Remove(dev);
      await _context.SaveChangesAsync();
      return NoContent();
    }
  }
}