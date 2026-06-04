using BrCitizenRegistry.Api.DTOs;
using BrCitizenRegistry.Api.Models;
using BrCitizenRegistry.Api.Repositories;
using BrCitizenRegistry.Api.Validators;

namespace BrCitizenRegistry.Api.Services;

public class CitizenService : ICitizenService
{
    private readonly ICitizenRepository _citizenRepository;

    public CitizenService(ICitizenRepository citizenRepository)
    {
        _citizenRepository = citizenRepository;
    }

    public async Task<CitizenResponse> CreateAsync(CreateCitizenRequest request)
    {
        var fullName = request.FullName.Trim();
        var cleanCpf = CpfValidator.RemoveMask(request.Cpf);

        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new InvalidOperationException("O nome completo é obrigatório.");
        }

        if (!CpfValidator.IsValid(cleanCpf))
        {
            throw new InvalidOperationException("CPF inválido.");
        }

        var cpfAlreadyExists = await _citizenRepository.ExistsByCpfAsync(cleanCpf);

        if (cpfAlreadyExists)
        {
            throw new InvalidOperationException("CPF já cadastrado.");
        }

        var citizen = new Citizen(fullName, cleanCpf);

        await _citizenRepository.AddAsync(citizen);

        return MapToResponse(citizen);
    }

    public async Task<CitizenResponse?> SearchAsync(string term)
    {
        var cleanTerm = term.Trim();

        if (string.IsNullOrWhiteSpace(cleanTerm))
        {
            return null;
        }

        var searchTerm = CpfValidator.RemoveMask(cleanTerm);

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = cleanTerm;
        }

        var citizen = await _citizenRepository.SearchAsync(searchTerm);

        if (citizen is null)
        {
            return null;
        }

        return MapToResponse(citizen);
    }

    private static CitizenResponse MapToResponse(Citizen citizen)
    {
        return new CitizenResponse
        {
            Id = citizen.Id,
            FullName = citizen.FullName,
            Cpf = CpfValidator.Format(citizen.Cpf)
        };
    }
}