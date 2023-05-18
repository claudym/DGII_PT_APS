using Microsoft.AspNetCore.Mvc;
using DGIIAPP.Application.Interfaces.Services;
using DGIIAPP.Application.DTOs;

[ApiController]
[Route("api/[controller]")]
public class ContribuyentesController : ControllerBase
{
    private readonly IContribuyenteService _contribuyenteService;

    public ContribuyentesController(IContribuyenteService contribuyenteService)
    {
        _contribuyenteService = contribuyenteService;
    }

    /// <summary>
    /// Obtener Lista de Contribuyentes.
    /// </summary>
    /// <returns>Lista de Contribuyentes.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContribuyenteDTO>>> Contribuyentes()
    {
        IEnumerable<ContribuyenteDTO> contribuyentes = await  _contribuyenteService.GetContribuyentes();
        return Ok(contribuyentes);
    }
}
