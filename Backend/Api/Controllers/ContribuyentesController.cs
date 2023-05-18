using DGIIAPP.API.Data;
using DGIIAPP.API.Models;
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

    /// <summary>
    /// Obtener Lista de Contribuyentes.
    /// </summary>
    /// <returns>Lista de Contribuyentes.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Contribuyente>>> Contribuyentes()
    {
        IEnumerable<Contribuyente> contribuyentes = await  _dbContext.Contribuyentes.ToListAsync();
        return Ok(contribuyentes);
    }
}
