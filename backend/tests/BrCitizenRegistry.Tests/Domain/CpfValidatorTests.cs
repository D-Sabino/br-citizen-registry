using BrCitizenRegistry.Domain.ValueObjects;
using FluentAssertions;

namespace BrCitizenRegistry.Tests.Domain;

public class CpfValidatorTests
{
    [Theory]
    [InlineData("529.982.247-25")]
    [InlineData("52998224725")]
    public void IsValid_ShouldReturnTrue_WhenCpfIsValid(string cpf)
    {
        var result = CpfValidator.IsValid(cpf);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("111.111.111-11")]
    [InlineData("123.456.789-00")]
    [InlineData("123")]
    [InlineData("")]
    public void IsValid_ShouldReturnFalse_WhenCpfIsInvalid(string cpf)
    {
        var result = CpfValidator.IsValid(cpf);

        result.Should().BeFalse();
    }

    [Fact]
    public void RemoveMask_ShouldReturnOnlyNumbers()
    {
        var result = CpfValidator.RemoveMask("529.982.247-25");

        result.Should().Be("52998224725");
    }

    [Fact]
    public void Format_ShouldReturnFormattedCpf_WhenCpfHasElevenDigits()
    {
        var result = CpfValidator.Format("52998224725");

        result.Should().Be("529.982.247-25");
    }
}