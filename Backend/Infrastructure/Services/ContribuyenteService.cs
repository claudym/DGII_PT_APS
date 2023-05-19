using DGIIAPP.Application.DTOs;
using DGIIAPP.Application.Interfaces.Services;
using DGIIAPP.Domain.Models;
using DGIIAPP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DGIIAPP.Infrastructure.Services;

public class ContribuyenteService : IContribuyenteService {
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<ContribuyenteService> _logger;

    public ContribuyenteService(ApplicationDbContext dbContext, ILogger<ContribuyenteService> logger) {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<ResultDTO<IEnumerable<ContribuyenteDTO>>> GetContribuyente() {
        _logger.LogDebug("Buscando Listado de Contribuyente...");
        IEnumerable<Contribuyente> contribuyentes = await  _dbContext.Contribuyentes.ToListAsync();
        _logger.LogDebug("Listado de Contribuyente encontrados.");
        List<ContribuyenteDTO> contribuyentesDTO = new List<ContribuyenteDTO>();
        foreach(var contribuyente in contribuyentes) {
            contribuyentesDTO.Add(
                new ContribuyenteDTO {
                    RncCedula = contribuyente.RncCedula,
                    Nombre = contribuyente.Nombre,
                    Tipo = contribuyente.Tipo,
                    Estatus = contribuyente.Estatus
                }
            );
        }
        return ResultDTO<IEnumerable<ContribuyenteDTO>>.Valid(contribuyentesDTO);
    }
}
