using DGIIAPP.Infrastructure.Data;
using DGIIAPP.Domain.Models;
using DGIIAPP.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DGIIAPP.Application.Interfaces.Services;

[ApiController]
[Route("api/[controller]")]
public class ComprobanteFiscalController : ControllerBase {
    private readonly IComprobanteFiscalService _comprobanteFiscalService;

    public ComprobanteFiscalController(IComprobanteFiscalService comprobanteFiscalService) {
        _comprobanteFiscalService = comprobanteFiscalService;
    }

    /// <summary>
    /// Obtener Lista de Comprobantes Fiscales.
    /// </summary>
    /// <returns>Lista de Comprobantes Fiscales.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResultDTO<IEnumerable<ComprobanteFiscalDTO>>>> ComprobantesFiscales() {
        ResultDTO<IEnumerable<ComprobanteFiscalDTO>> comprobantesFiscales = await _comprobanteFiscalService.GetComprobantesFiscales();
        return Ok(comprobantesFiscales);
    }

    /// <summary>
    /// Obtener total de ITBIS para un contribuyente (RNC/Cedula).
    /// </summary>
    /// <param name="rncCedula"></param>
    /// <returns>Total de ITBIS para un contribuyente (RNC/Cedula).</returns>
    [HttpGet("{rncCedula}/ITBIS/Total")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResultDTO<TotalITBISDTO>>> TotalITBIS(string rncCedula) {
        ResultDTO<TotalITBISDTO> totalITBIS = await _comprobanteFiscalService.GetTotalITBIS(rncCedula);
        return Ok(totalITBIS);
    }

    /// <summary>
    /// Obtener Lista del Total de ITBIS por cada contribuyente (RNC/Cedula).
    /// </summary>
    /// <returns>Lista del Total de ITBIS por cada contribuyente (RNC/Cedula).</returns>
    [HttpGet("ITBIS/Total")]
    public async Task<ActionResult<ResultDTO<IEnumerable<TotalITBISDTO>>>> TotalITBIS() {
        ResultDTO<IEnumerable<TotalITBISDTO>> totalITBISList = await _comprobanteFiscalService.GetTotalITBISList();
        return Ok(totalITBISList);
    }
}
