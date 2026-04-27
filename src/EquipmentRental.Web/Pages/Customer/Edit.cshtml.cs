using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using EquipmentRental.Domain.entities;
using EquipmentRental.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace EquipmentRental.Web.Pages.Customer;

[Authorize(Roles ="Admin,Employee")]
public class EditModel : PageModel
{
    private readonly ICustomerRepository _repository;

    [BindProperty]
    public Domain.entities.Customer Customer { get; set; } = new();

    public EditModel(ICustomerRepository repository)
    {
        _repository = repository;
    }
    public async Task<IActionResult> OnGetAsync(int id)
    {
        var customer = await _repository.GetByIdAsync(id);
        if (customer == null)
            return NotFound();
        Customer = customer;

        return Page();
        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        await _repository.UpdateCustomerAsync(Customer);
        return RedirectToPage("Index");
    }
}