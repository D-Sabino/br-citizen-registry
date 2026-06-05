using BrCitizenRegistry.Application.DTOs;
using BrCitizenRegistry.Application.Ports.Out;
using BrCitizenRegistry.Application.UseCases;
using BrCitizenRegistry.Domain.Entities;
using FluentAssertions;
using NSubstitute;

namespace BrCitizenRegistry.Tests.Application;

public class CitizenUseCaseTests
{
    private readonly ICitizenRepository _citizenRepository;
    private readonly CitizenUseCase _citizenUseCase;

    public CitizenUseCaseTests()
    {
        _citizenRepository = Substitute.For<ICitizenRepository>();
        _citizenUseCase = new CitizenUseCase(_citizenRepository);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateCitizen_WhenDataIsValid()
    {
        var request = new CreateCitizenRequest
        {
            FullName = "João da Silva",
            Cpf = "529.982.247-25"
        };

        _citizenRepository
            .ExistsByCpfAsync("52998224725")
            .Returns(Task.FromResult(false));

        _citizenRepository
            .AddAsync(Arg.Any<Citizen>())
            .Returns(Task.CompletedTask);

        var result = await _citizenUseCase.CreateAsync(request);

        result.Should().NotBeNull();
        result.FullName.Should().Be("João da Silva");
        result.Cpf.Should().Be("529.982.247-25");

        await _citizenRepository
            .Received(1)
            .AddAsync(Arg.Is<Citizen>(citizen =>
                citizen.FullName == "João da Silva" &&
                citizen.Cpf == "52998224725"
            ));
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenCpfIsInvalid()
    {
        var request = new CreateCitizenRequest
        {
            FullName = "Maria Souza",
            Cpf = "111.111.111-11"
        };

        var action = async () => await _citizenUseCase.CreateAsync(request);

        await action
            .Should()
            .ThrowAsync<InvalidOperationException>()
            .WithMessage("CPF inválido.");

        await _citizenRepository
            .DidNotReceive()
            .AddAsync(Arg.Any<Citizen>());
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenCpfAlreadyExists()
    {
        var request = new CreateCitizenRequest
        {
            FullName = "João da Silva",
            Cpf = "529.982.247-25"
        };

        _citizenRepository
            .ExistsByCpfAsync("52998224725")
            .Returns(Task.FromResult(true));

        var action = async () => await _citizenUseCase.CreateAsync(request);

        await action
            .Should()
            .ThrowAsync<InvalidOperationException>()
            .WithMessage("CPF já cadastrado.");

        await _citizenRepository
            .DidNotReceive()
            .AddAsync(Arg.Any<Citizen>());
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnCitizens_WhenNameMatches()
    {
        var citizens = new List<Citizen>
        {
            new("João da Silva", "52998224725"),
            new("João da Silva", "39053344705")
        };

        _citizenRepository
            .SearchAsync("João")
            .Returns(Task.FromResult(citizens));

        var result = await _citizenUseCase.SearchAsync("João");

        result.Should().HaveCount(2);
        result[0].FullName.Should().Be("João da Silva");
        result[0].Cpf.Should().Be("529.982.247-25");
        result[1].Cpf.Should().Be("390.533.447-05");
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnCitizen_WhenCpfMatches()
    {
        var citizens = new List<Citizen>
        {
            new("João da Silva", "52998224725")
        };

        _citizenRepository
            .SearchAsync("52998224725")
            .Returns(Task.FromResult(citizens));

        var result = await _citizenUseCase.SearchAsync("529.982.247-25");

        result.Should().ContainSingle();
        result[0].FullName.Should().Be("João da Silva");
        result[0].Cpf.Should().Be("529.982.247-25");
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnEmptyList_WhenNoCitizenMatches()
    {
        _citizenRepository
            .SearchAsync("Pessoa inexistente")
            .Returns(Task.FromResult(new List<Citizen>()));

        var result = await _citizenUseCase.SearchAsync("Pessoa inexistente");

        result.Should().BeEmpty();
    }
}