namespace BrCitizenRegistry.Api.DTOs;

public class CitizenResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
}