using EquipmentRental.Application.Interfaces;
using EquipmentRental.Domain.entities;
using EquipmentRental.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EquipmentRental.Infrastructure.Repositories;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly AppDbContext _context;

    public EquipmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Equipment>> GetAllAsync() =>
        await _context.Equipment.ToListAsync();

    public async Task<IEnumerable<Equipment>> GetAvailableAsync() =>
        await _context.Equipment.Where(e => e.IsAvailable).ToListAsync();

    public async Task<Equipment?> GetByIdAsync(int id) =>
        await _context.Equipment.FindAsync(id);

    public async Task AddAsync(Equipment equipment)
    {
        _context.Equipment.Add(equipment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Equipment equipment)
    {
        _context.Equipment.Update(equipment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var equipment = await _context.Equipment.FindAsync(id);
        if (equipment != null)
        {
            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();
        }
    }
    
}