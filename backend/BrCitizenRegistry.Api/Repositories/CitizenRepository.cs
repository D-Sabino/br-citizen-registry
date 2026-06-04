using BrCitizenRegistry.Api.Data;
using BrCitizenRegistry.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BrCitizenRegistry.Api.Repositories;

public class CitizenRepository : ICitizenRepository
{
    private readonly AppDbContext _context;

    public CitizenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Citizen citizen)
    {
        await _context.Citizens.AddAsync(citizen);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByCpfAsync(string cpf)
    {
        return await _context.Citizens
            .AnyAsync(citizen => citizen.Cpf == cpf);
    }

    public async Task<Citizen?> GetByCpfAsync(string cpf)
    {
        return await _context.Citizens
            .FirstOrDefaultAsync(citizen => citizen.Cpf == cpf);
    }

    public async Task<Citizen?> SearchAsync(string term)
    {
        return await _context.Citizens
            .FirstOrDefaultAsync(citizen =>
                citizen.Cpf == term ||
                EF.Functions.Like(citizen.FullName, $"%{term}%"));
    }
}