using EquipmentRental.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EquipmentRental.Web.Pages.Customer;

[Authorize(Roles ="Admin,Employee")]
public class CreateModel : PageModel
{
    private readonly ICustomerRepository _repository;

    [BindProperty] 
    public Domain.entities.Customer Customer { get; set; } = new();
    
    public CreateModel(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        
        await _repository.AddNewCustomerAsync(Customer);
        return RedirectToPage("Index");
            
    }
    
    



}