namespace BrCitizenRegistry.Domain.Entities;

public class Citizen
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; } = string.Empty;
    public string Cpf { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    private Citizen()
    {
    }

    public Citizen(string fullName, string cpf)
    {
        Id = Guid.NewGuid();
        FullName = fullName.Trim();
        Cpf = cpf;
        CreatedAt = DateTime.UtcNow;
    }
}