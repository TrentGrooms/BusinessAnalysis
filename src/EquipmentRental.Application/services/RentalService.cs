using System.Reflection;
using EquipmentRental.Application.Interfaces;
using EquipmentRental.Domain.entities;


namespace EquipmentRental.Application.services;

public class RentalService
{
    private readonly IRentalRepository _rentalRepo;
    private readonly IEquipmentRepository _equipmentRepo;

    private const decimal LateFeePerDay = 20m;

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

    public decimal CalculateTotalCost(Rental rental, DateTime returnDate)
    {
        var rentalDays = (returnDate - rental.RentalDate).Days;
        if (rentalDays < 1) rentalDays = 1;

        var baseCost = rentalDays * rental.Equipment.DailyRate;

        var daysLate = (returnDate - rental.DueDate).Days;
        var lateFee = daysLate > 0 ? daysLate * LateFeePerDay : 0m;

        return baseCost + lateFee;

    }

     public async Task<RevenueReport> GetRevenueReportAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        var rentals = await _rentalRepo.GetClosedAsync();

        if (startDate.HasValue)
            rentals = rentals.Where(r => r.ReturnDate.HasValue && r.ReturnDate.Value.Date >= startDate.Value.Date);

        if (endDate.HasValue)
            rentals = rentals.Where(r => r.ReturnDate.HasValue && r.ReturnDate.Value.Date <= endDate.Value.Date);

        var closedRentals = rentals.ToList();

        var report = new RevenueReport
        {
            TotalRevenue = closedRentals.Sum(r => r.TotalCost),
            RentalCount = closedRentals.Count,
            ByMonth = closedRentals
                .Where(r => r.ReturnDate.HasValue)
                .GroupBy(r => new { r.ReturnDate.Value.Year, r.ReturnDate.Value.Month })
                .Select(g => new RevenueByMonth
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(r => r.TotalCost),
                    RentalCount = g.Count()
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToList()
        };

        return report;
    }


    public class RevenueReport
    {
        public decimal TotalRevenue { get; set; }
        public int RentalCount { get; set; }
        public decimal AverageRevenuePerRental => RentalCount == 0 ? 0m : TotalRevenue / RentalCount;
        public List<RevenueByMonth> ByMonth { get; set; } = new();
    }

    public class RevenueByMonth
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Revenue { get; set; }
        public int RentalCount { get; set; }
    }

    
    
}