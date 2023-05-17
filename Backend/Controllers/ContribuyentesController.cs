using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ContribuyentesController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public ContribuyentesController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Contribuyente>>> GetContribuyentes()
    {
        IEnumerable<Contribuyente> contribuyentes = await  _dbContext.Contribuyentes.ToListAsync();
        return Ok(contribuyentes);
    }
}
