using BrCitizenRegistry.Application.Ports.Out;
using BrCitizenRegistry.Domain.Entities;
using BrCitizenRegistry.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BrCitizenRegistry.Infrastructure.Persistence.Repositories;

public class MySqlCitizenRepository : ICitizenRepository
{
    private readonly AppDbContext _context;

    public MySqlCitizenRepository(AppDbContext context)
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

    public async Task<List<Citizen>> SearchAsync(string term)
    {
        return await _context.Citizens
            .Where(citizen =>
                citizen.Cpf == term ||
                EF.Functions.Like(citizen.FullName, $"%{term}%"))
            .OrderBy(citizen => citizen.FullName)
            .ToListAsync();
    }
}