using Backend.Data;
using Backend.Models;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComprobanteFiscal>>> GetComprobantesFiscales()
    {
        IEnumerable<ComprobanteFiscal> comprobantesFiscales = await _dbContext.ComprobantesFiscales.ToListAsync();
        return Ok(comprobantesFiscales);
    }

    [HttpGet("{rncCedula}/total-itbis")]
    public ActionResult<TotalITBISPorRnc> GetTotalITBIS(string rncCedula)
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

    [HttpGet("total-itbis-por-rnc")]
    public ActionResult<IEnumerable<TotalITBISPorRnc>> GetTotalITBISPorRnc()
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
