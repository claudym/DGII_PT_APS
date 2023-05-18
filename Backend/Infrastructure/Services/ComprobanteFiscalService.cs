using DGIIAPP.Application.DTOs;
using DGIIAPP.Application.Interfaces.Services;
using DGIIAPP.Domain.Models;
using DGIIAPP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DGIIAPP.Infrastructure.Services;

public class ComprobanteFiscalService : IComprobanteFiscalService {
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<ComprobanteFiscalService> _logger;

    public ComprobanteFiscalService(ApplicationDbContext dbContext, ILogger<ComprobanteFiscalService> logger) {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<ResultDTO<IEnumerable<ComprobanteFiscalDTO>>> GetComprobantesFiscales() {
        _logger.LogDebug("Buscando Listado de Comprobantes Fiscales...");
        IEnumerable<ComprobanteFiscal> comprobantesfiscales = await  _dbContext.ComprobantesFiscales.ToListAsync();
        _logger.LogDebug("Listado de Comprobantes Fiscales encontrados.");

        List<ComprobanteFiscalDTO> comprobantesDTO = new List<ComprobanteFiscalDTO>();
        foreach(var comprobantefiscal in comprobantesfiscales) {
            comprobantesDTO.Add(
                new ComprobanteFiscalDTO {
                    NCF = comprobantefiscal.NCF,
                    RncCedula = comprobantefiscal.RncCedula,
                    Monto = comprobantefiscal.Monto,
                    Itbis18 = comprobantefiscal.Itbis18
                }
            );
        }
        return ResultDTO<IEnumerable<ComprobanteFiscalDTO>>.Valid(comprobantesDTO);
    }

    public async Task<ResultDTO<TotalITBISDTO>> GetTotalITBIS(string rncCedula) {
        decimal totalITBIS = _dbContext.ComprobantesFiscales
            .Where(cf => cf.RncCedula == rncCedula)
            .Sum(cf => cf.Itbis18);
        _logger.LogDebug("Buscando el Total de ITBIS para un contribuyente...");
        Contribuyente? contribuyente = await _dbContext.Contribuyentes.FirstOrDefaultAsync(c => c.RncCedula == rncCedula);
        if (contribuyente == null) {
            _logger.LogError($"Total de ITBIS: Registro con RNC/Cédula {rncCedula} no existe.");
            throw new NotFoundException($"Registro con RNC/Cédula {rncCedula} no existe.");
        }
        _logger.LogDebug("Total de ITBIS para un contribuyente encontrado.");
        TotalITBISDTO totalITBISPorRnc = new TotalITBISDTO {
            RncCedula = rncCedula,
            NombreContribuyente = contribuyente.Nombre,
            TotalITBIS18 = totalITBIS
        };
        return ResultDTO<TotalITBISDTO>.Valid(totalITBISPorRnc);
    }

    public async Task<ResultDTO<IEnumerable<TotalITBISDTO>>> GetTotalITBISList() {
        _logger.LogDebug("Buscando Listado del Total de ITBIS por cada contribuyente...");
        IEnumerable<TotalITBISDTO> totalITBISPorRnc = await _dbContext.ComprobantesFiscales
            .GroupBy(cf => cf.RncCedula)
            .Select(group => new TotalITBISDTO {
                RncCedula = group.Key,
                NombreContribuyente = _dbContext.Contribuyentes.FirstOrDefault(c => c.RncCedula == group.Key)!.Nombre,
                TotalITBIS18 = group.Sum(cf => cf.Itbis18)
            })
            .ToListAsync();
        _logger.LogDebug("Listado del Total de ITBIS por cada contribuyente encontrado.");
        return ResultDTO<IEnumerable<TotalITBISDTO>>.Valid(totalITBISPorRnc);
    }
}