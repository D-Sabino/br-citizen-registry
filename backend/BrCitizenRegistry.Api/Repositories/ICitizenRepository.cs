using BrCitizenRegistry.Api.Models;

namespace BrCitizenRegistry.Api.Repositories;

public interface ICitizenRepository
{
    Task AddAsync(Citizen citizen);
    Task<bool> ExistsByCpfAsync(string cpf);
    Task<Citizen?> GetByCpfAsync(string cpf);
    Task<Citizen?> SearchAsync(string term);
}