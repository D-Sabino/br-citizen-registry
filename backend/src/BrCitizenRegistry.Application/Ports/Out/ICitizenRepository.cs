using BrCitizenRegistry.Domain.Entities;

namespace BrCitizenRegistry.Application.Ports.Out;

public interface ICitizenRepository
{
    Task AddAsync(Citizen citizen);
    Task<bool> ExistsByCpfAsync(string cpf);
    Task<Citizen?> GetByCpfAsync(string cpf);
    Task<List<Citizen>> SearchAsync(string term);
}