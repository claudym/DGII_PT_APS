using DGIIAPP.Application.DTOs;
using DGIIAPP.Application.Interfaces.Services;
using DGIIAPP.Domain.Models;
using DGIIAPP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DGIIAPP.Infrastructure.Services;

public class ComprobanteFiscalService : IComprobanteFiscalService {
    private readonly ApplicationDbContext _dbContext;

    public ComprobanteFiscalService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ComprobanteFiscalDTO>> GetComprobantesFiscales() {
        IEnumerable<ComprobanteFiscal> comprobantesfiscales = await  _dbContext.ComprobantesFiscales.ToListAsync();
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
        return comprobantesDTO;
    }

    public async Task<TotalITBISDTO> GetTotalITBIS(string rncCedula) {
        decimal totalITBIS = _dbContext.ComprobantesFiscales
            .Where(cf => cf.RncCedula == rncCedula)
            .Sum(cf => cf.Itbis18);
        Contribuyente? contribuyente = await _dbContext.Contribuyentes.FirstOrDefaultAsync(c => c.RncCedula == rncCedula);
        if (contribuyente == null) return null;
        TotalITBISDTO totalITBISPorRnc = new TotalITBISDTO {
            RncCedula = rncCedula,
            NombreContribuyente = contribuyente.Nombre,
            TotalITBIS18 = totalITBIS
        };
        return totalITBISPorRnc;
    }

    public async Task<IEnumerable<TotalITBISDTO>> GetTotalITBISList() {
        IEnumerable<TotalITBISDTO> totalITBISPorRnc = await _dbContext.ComprobantesFiscales
            .GroupBy(cf => cf.RncCedula)
            .Select(group => new TotalITBISDTO
            {
                RncCedula = group.Key,
                NombreContribuyente = _dbContext.Contribuyentes.FirstOrDefault(c => c.RncCedula == group.Key)!.Nombre,
                TotalITBIS18 = group.Sum(cf => cf.Itbis18)
            })
            .ToListAsync();
        return totalITBISPorRnc;
    }
}