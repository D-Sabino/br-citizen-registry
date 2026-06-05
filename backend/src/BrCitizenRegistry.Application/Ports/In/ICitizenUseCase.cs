using BrCitizenRegistry.Application.DTOs;

namespace BrCitizenRegistry.Application.Ports.In;

public interface ICitizenUseCase
{
    Task<CitizenResponse> CreateAsync(CreateCitizenRequest request);
    Task<List<CitizenResponse>> SearchAsync(string term);
}