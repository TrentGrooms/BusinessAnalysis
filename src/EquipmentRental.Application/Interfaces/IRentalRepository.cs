using EquipmentRental.Domain.entities;

namespace EquipmentRental.Application.Interfaces;

public interface IRentalRepository
{
    Task<IEnumerable<Rental>> GetAllAsync();

    Task<IEnumerable<Rental>> GetActiveAsync();
    
    Task<IEnumerable<Rental>> GetLateAsync();
    
    Task<IEnumerable<Rental>> GetClosedAsync();
    
    Task<Rental> GetByIdAsync(int id);
    
    Task AddAsync(Rental rental);
    
    Task UpdateAsync(Rental rental);
    
}