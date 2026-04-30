using EquipmentRental.Application.Interfaces;
using EquipmentRental.Domain.entities;
using EquipmentRental.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EquipmentRental.Infrastructure.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly AppDbContext _context;

    public RentalRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Rental>> GetAllAsync() =>
    
        await _context.Rentals
            .Include(r => r.Equipment)
            .Include(r => r.Customer)
            .OrderByDescending(r => r.RentalDate)
            .ToListAsync();
    

    public async Task<IEnumerable<Rental>> GetActiveAsync() =>
        await _context.Rentals
            .Include(r => r.Equipment)
            .Include(r => r.Customer)
            .Where(r => !r.IsClosed)
            .ToListAsync();

    public async Task<IEnumerable<Rental>> GetLateAsync() =>
    
        await _context.Rentals
            .Include(r => r.Equipment)
            .Include(r => r.Customer)
            .Where(r => !r.IsClosed && r.DueDate < DateTime.Today)
            .ToListAsync();
    

    public async Task<IEnumerable<Rental>> GetClosedAsync() =>
        await _context.Rentals
            .Where(r => r.IsClosed)
            .ToListAsync();
            

    public async Task<Rental> GetByIdAsync(int id) =>
        await _context.Rentals
            .Include(r => r.Equipment)
            .Include(r => r.Customer)
            .FirstOrDefaultAsync(r => r.RentalId == id);

    public async Task AddAsync(Rental rental)
    {
        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Rental rental)
    {
        _context.Rentals.Update(rental);
        await _context.SaveChangesAsync();
    }
}