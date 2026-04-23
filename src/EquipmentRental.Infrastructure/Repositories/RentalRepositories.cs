using EquipmentRental.Application.Interfaces;
using EquipmentRental.Domain.entities;

namespace EquipmentRental.Infrastructure.Repositories;

public class RentalRepositories : IRentalRepository
{
    public Task<IEnumerable<Rental>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Rental>> GetActiveAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Rental>> GetLateAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Rental>> GetClosedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Rental> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Rental rental)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Rental rental)
    {
        throw new NotImplementedException();
    }
}