using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EquipmentRental.Application.Interfaces;
using EquipmentRental.Application.services;
using EquipmentRental.Domain.entities;

namespace EquipmentRental.Web.Pages.Rental;

[Authorize(Roles = "Admin,Employee")]
public class IndexModel : PageModel
{
    private readonly IRentalRepository _rentalRepository;
    private readonly RentalService _rentalService;

    public IEnumerable<Domain.entities.Rental> Rentals { get; set; } = new List<Domain.entities.Rental>();

    [BindProperty(SupportsGet = true)]
    public string Filter { get; set; } = "all";

    public string? SuccessMessage { get; set; }
    public string? ErrorMessage { get; set; }

    public IndexModel(IRentalRepository rentalRepository, RentalService rentalService)
    {
        _rentalRepository = rentalRepository;
        _rentalService = rentalService;
    }

    public async Task OnGetAsync()
    {
        Rentals = Filter switch
        {
            "active" => await _rentalRepository.GetActiveAsync(),
            "late" => await _rentalRepository.GetLateAsync(),
            "closed" => await _rentalRepository.GetClosedAsync(),
            _ => await _rentalRepository.GetAllAsync()
        };
    }

    public async Task<IActionResult> OnPostCloseAsync(int id)
    {
        var (success, error) = await _rentalService.CloseAsync(id);

        if (!success)
        {
            ErrorMessage = error;
            Rentals = await _rentalRepository.GetAllAsync();
            return Page();
        }

        return RedirectToPage("Index", new { filter = Filter });
    }
}
