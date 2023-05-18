using DGIIAPP.Application.DTOs;

namespace DGIIAPP.Application.Interfaces.Services;

public interface IComprobanteFiscalService {
    Task<IEnumerable<ComprobanteFiscalDTO>> GetComprobantesFiscales();
    Task<TotalITBISDTO> GetTotalITBIS(string rncCedula);
    Task<IEnumerable<TotalITBISDTO>> GetTotalITBISList();
}