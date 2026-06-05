using BrCitizenRegistry.Application.Validators;
using BrCitizenRegistry.Application.DTOs;
using BrCitizenRegistry.Application.Ports.In;
using BrCitizenRegistry.Application.Ports.Out;
using BrCitizenRegistry.Domain.Entities;
using BrCitizenRegistry.Domain.ValueObjects;

namespace BrCitizenRegistry.Application.UseCases;

public class CitizenUseCase : ICitizenUseCase
{
    private readonly ICitizenRepository _citizenRepository;

    public CitizenUseCase(ICitizenRepository citizenRepository)
    {
        _citizenRepository = citizenRepository;
    }

    public async Task<CitizenResponse> CreateAsync(CreateCitizenRequest request)
    {
        var fullName = request.FullName.Trim();
        var cleanCpf = CpfValidator.RemoveMask(request.Cpf);

        if (!CitizenInputValidator.IsValidFullName(fullName))
        {
            throw new InvalidOperationException("O nome completo contém caracteres inválidos.");
        }

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

    public async Task<List<CitizenResponse>> SearchAsync(string term)
    {
        var cleanTerm = term.Trim();

        if (string.IsNullOrWhiteSpace(cleanTerm))
        {
            return [];
        }

        if (!CitizenInputValidator.IsValidSearchTerm(cleanTerm))
        {
            throw new InvalidOperationException("O termo de pesquisa contém caracteres inválidos.");
        }

        var searchTerm = CpfValidator.RemoveMask(cleanTerm);

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = cleanTerm;
        }

        var citizens = await _citizenRepository.SearchAsync(searchTerm);

        return citizens
            .Select(MapToResponse)
            .ToList();
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