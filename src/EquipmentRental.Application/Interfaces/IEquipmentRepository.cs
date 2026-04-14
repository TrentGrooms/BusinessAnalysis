using EquipmentRental.Domain.entities;

namespace EquipmentRental.Application.Interfaces;

public interface IEquipmentRepository
{
    Task<IEnumerable<Equipment>> GetAllAsync();
    Task<IEnumerable<Equipment>> GetAvailableAsync();
    Task<Equipment?> GetByIdAsync(int id);
    Task AddAsync(Equipment equipment);
    Task UpdateAsync(Equipment equipment);
    Task DeleteAsync(int id);
}