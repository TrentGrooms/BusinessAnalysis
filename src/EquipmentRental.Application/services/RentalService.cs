using EquipmentRental.Application.Interfaces;
using EquipmentRental.Domain.entities;


namespace EquipmentRental.Application.services;

public class RentalService
{
    private readonly IRentalRepository _rentalRepo;
    private readonly IEquipmentRepository _equipmentRepo;

    public RentalService(IRentalRepository rentalRepo, IEquipmentRepository equipmentRepo)
    {
        _rentalRepo = rentalRepo;
        _equipmentRepo = equipmentRepo;

    }

    public async Task<(bool Success, string Error)> CreateAsync(Rental rental)
    {
        var equipment = await _equipmentRepo.GetByIdAsync(rental.EquipmentId);

        if (equipment == null)
            return (false, "Equipment not found.");

        if (!equipment.IsAvailable)
            return (false, "This equipment is not available for rental.");

        rental.RentalDate = DateTime.Today;
        rental.IsClosed = false;

        equipment.IsAvailable = false;

        await _equipmentRepo.UpdateAsync(equipment);
        await _rentalRepo.AddAsync(rental);

        return (true, string.Empty);
    }

    public async Task<(bool Success, string Error)> CloseAsync(int rentalId)
    {
        var rental = await _rentalRepo.GetByIdAsync(rentalId);

        if (rental == null)
            return (false, "Rental not found.");

        if (rental.IsClosed)
            return (false, "Rental is already closed.");

        var returnDate = DateTime.Today;
        rental.ReturnDate = returnDate;
        rental.IsClosed = true;
        rental.TotalCost = CalculateTotalCost(rental, returnDate);
        rental.Equipment.IsAvailable = true;

        await _rentalRepo.UpdateAsync(rental);
        await _equipmentRepo.UpdateAsync(rental.Equipment);

        return (true, string.Empty);
    }

    
    
}