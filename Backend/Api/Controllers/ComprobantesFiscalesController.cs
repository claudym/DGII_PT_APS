using DGIIAPP.Infrastructure.Data;
using DGIIAPP.Domain.Models;
using DGIIAPP.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DGIIAPP.Application.Interfaces.Services;

[ApiController]
[Route("api/[controller]")]
public class ComprobantesFiscalesController : ControllerBase
{
    private readonly IComprobanteFiscalService _comprobanteFiscalService;

    public ComprobantesFiscalesController(IComprobanteFiscalService comprobanteFiscalService)
    {
        _comprobanteFiscalService = comprobanteFiscalService;
    }

    /// <summary>
    /// Obtener Lista de Comprobantes Fiscales.
    /// </summary>
    /// <returns>Lista de Comprobantes Fiscales.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ComprobanteFiscalDTO>>> ComprobantesFiscales()
    {
        IEnumerable<ComprobanteFiscalDTO> comprobantesFiscales = await _comprobanteFiscalService.GetComprobantesFiscales();
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
    public async Task<ActionResult<TotalITBISDTO>> TotalITBIS(string rncCedula)
    {
        TotalITBISDTO totalITBIS = await _comprobanteFiscalService.GetTotalITBIS(rncCedula);
        return (totalITBIS is null) ? NotFound() : Ok(totalITBIS);
    }

    /// <summary>
    /// Obtener Lista del Total de ITBIS por cada contribuyente (RNC/Cedula).
    /// </summary>
    /// <returns>Lista del Total de ITBIS por cada contribuyente (RNC/Cedula).</returns>
    [HttpGet("ITBIS/Total")]
    public async Task<ActionResult<IEnumerable<TotalITBISDTO>>> TotalITBIS()
    {
        IEnumerable<TotalITBISDTO> totalITBISList = await _comprobanteFiscalService.GetTotalITBISList();
        return Ok(totalITBISList);
    }

}
