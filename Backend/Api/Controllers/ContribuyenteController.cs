using Microsoft.AspNetCore.Mvc;
using DGIIAPP.Application.Interfaces.Services;
using DGIIAPP.Application.DTOs;

[ApiController]
[Route("api/[controller]")]
public class ContribuyenteController : ControllerBase
{
    private readonly IContribuyenteService _contribuyenteService;

    public ContribuyenteController(IContribuyenteService contribuyenteService) {
        _contribuyenteService = contribuyenteService;
    }

    /// <summary>
    /// Obtener Lista de Contribuyentes.
    /// </summary>
    /// <returns>Lista de Contribuyentes.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResultDTO<IEnumerable<ContribuyenteDTO>>>> Contribuyentes() {
        ResultDTO<IEnumerable<ContribuyenteDTO>> contribuyentes = await  _contribuyenteService.GetContribuyentes();
        return Ok(contribuyentes);
    }
}
