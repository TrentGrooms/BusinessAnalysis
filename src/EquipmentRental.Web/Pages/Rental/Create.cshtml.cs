using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EquipmentRental.Application.Interfaces;
using EquipmentRental.Application.services;
using EquipmentRental.Domain.entities;

namespace EquipmentRental.Web.Pages.Rental;

[Authorize(Roles = "Admin,Employee")]
public class CreateModel : PageModel
{
    private readonly RentalService _rentalService;
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly ICustomerRepository _customerRepository;

    [BindProperty]
    public Domain.entities.Rental Rental { get; set; } = new();

    public IEnumerable<Domain.entities.Equipment> AvailableEquipment { get; set; } = new List<Domain.entities.Equipment>();
    public IEnumerable<Domain.entities.Customer> Customers { get; set; } = new List<Domain.entities.Customer>();

    public string? ErrorMessage { get; set; }

    public CreateModel(
        RentalService rentalService,
        IEquipmentRepository equipmentRepository,
        ICustomerRepository customerRepository)
    {
        _rentalService = rentalService;
        _equipmentRepository = equipmentRepository;
        _customerRepository = customerRepository;
    }

    public async Task OnGetAsync()
    {
        AvailableEquipment = await _equipmentRepository.GetAvailableAsync();
        Customers = await _customerRepository.GetAllCustomersAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Rental.EquipmentId == 0 || Rental.CustomerId == 0 || Rental.DueDate == default)
        {
            ErrorMessage = "Please fill in all required fields.";
            AvailableEquipment = await _equipmentRepository.GetAvailableAsync();
            Customers = await _customerRepository.GetAllCustomersAsync();
            return Page();
        }

        var (success, error) = await _rentalService.CreateAsync(Rental);

        if (!success)
        {
            ErrorMessage = error;
            AvailableEquipment = await _equipmentRepository.GetAvailableAsync();
            Customers = await _customerRepository.GetAllCustomersAsync();
            return Page();
        }

        return RedirectToPage("Index");
    }
}
