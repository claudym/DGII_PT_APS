using DGIIAPP.Application.DTOs;

namespace DGIIAPP.Application.Interfaces.Services;

public interface IContribuyenteService {
    Task<IEnumerable<ContribuyenteDTO>> GetContribuyentes();
}