using DGIIAPP.Application.DTOs;

namespace DGIIAPP.Application.Interfaces.Services;

public interface IComprobanteFiscalService {
    Task<ResultDTO<IEnumerable<ComprobanteFiscalDTO>>> GetComprobanteFiscal();
    Task<ResultDTO<TotalITBISDTO>> GetTotalITBIS(string rncCedula);
    Task<ResultDTO<IEnumerable<TotalITBISDTO>>> GetTotalITBISList();
}