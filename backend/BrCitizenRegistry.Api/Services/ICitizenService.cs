using BrCitizenRegistry.Api.DTOs;

namespace BrCitizenRegistry.Api.Services;

public interface ICitizenService
{
    Task<CitizenResponse> CreateAsync(CreateCitizenRequest request);
    Task<List<CitizenResponse?>> SearchAsync(string term);
}