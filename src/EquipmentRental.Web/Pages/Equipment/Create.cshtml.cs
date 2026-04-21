using Microsoft.AspNetCore.Mvc.RazorPages;
using EquipmentRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.Web.Pages.Equipment;

public class CreateModel : PageModel
{
    private readonly IEquipmentRepository _repository;

    [BindProperty] 
    public Domain.entities.Equipment Equipment { get; set; } = new();

    public CreateModel(IEquipmentRepository repository)
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
        
        await _repository.AddAsync(Equipment);
        return RedirectToPage("Index");
    }



}