using DGIIAPP.Application.DTOs;
using DGIIAPP.Application.Interfaces.Services;
using DGIIAPP.Domain.Models;
using DGIIAPP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DGIIAPP.Infrastructure.Services;

public class ContribuyenteService : IContribuyenteService {
    private readonly ApplicationDbContext _dbContext;

    public ContribuyenteService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ContribuyenteDTO>> GetContribuyentes() {
        IEnumerable<Contribuyente> contribuyentes = await  _dbContext.Contribuyentes.ToListAsync();
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
        return contribuyentesDTO;
    }
}