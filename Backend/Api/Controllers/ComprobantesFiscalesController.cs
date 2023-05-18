using DGIIAPP.API.Data;
using DGIIAPP.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ComprobantesFiscalesController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public ComprobantesFiscalesController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Obtener Lista de Comprobantes Fiscales.
    /// </summary>
    /// <returns>Lista de Comprobantes Fiscales.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ComprobanteFiscal>>> ComprobantesFiscales()
    {
        IEnumerable<ComprobanteFiscal> comprobantesFiscales = await _dbContext.ComprobantesFiscales.ToListAsync();
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
    public ActionResult<TotalITBISPorRnc> TotalITBIS(string rncCedula)
    {
        decimal totalITBIS = _dbContext.ComprobantesFiscales
            .Where(cf => cf.RncCedula == rncCedula)
            .Sum(cf => cf.Itbis18);

        Contribuyente? contribuyente = _dbContext.Contribuyentes.FirstOrDefault(c => c.RncCedula == rncCedula);

        if (contribuyente == null)
        {
            return NotFound();
        }

        TotalITBISPorRnc totalITBISPorRnc = new TotalITBISPorRnc
        {
            RncCedula = rncCedula,
            NombreContribuyente = contribuyente.Nombre,
            TotalITBIS = totalITBIS
        };

        return Ok(totalITBISPorRnc);
    }

    /// <summary>
    /// Obtener Lista del Total de ITBIS por cada contribuyente (RNC/Cedula).
    /// </summary>
    /// <returns>Lista del Total de ITBIS por cada contribuyente (RNC/Cedula).</returns>
    [HttpGet("ITBIS/Total")]
    public ActionResult<IEnumerable<TotalITBISPorRnc>> TotalITBIS()
    {
        IEnumerable<TotalITBISPorRnc> totalITBISPorRnc = _dbContext.ComprobantesFiscales
            .GroupBy(cf => cf.RncCedula)
            .Select(group => new TotalITBISPorRnc
            {
                RncCedula = group.Key,
                NombreContribuyente = _dbContext.Contribuyentes.FirstOrDefault(c => c.RncCedula == group.Key)!.Nombre,
                TotalITBIS = group.Sum(cf => cf.Itbis18)
            })
            .ToList();
        return Ok(totalITBISPorRnc);
    }

}
