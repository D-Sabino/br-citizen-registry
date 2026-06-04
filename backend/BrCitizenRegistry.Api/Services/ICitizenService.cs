using BrCitizenRegistry.Api.DTOs;

namespace BrCitizenRegistry.Api.Services;

public interface ICitizenService
{
    Task<CitizenResponse> CreateAsync(CreateCitizenRequest request);
    Task<CitizenResponse?> SearchAsync(string term);
}