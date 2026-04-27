using Microsoft.AspNetCore.Mvc.RazorPages;
using EquipmentRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EquipmentRental.Web.Pages.Equipment;

[Authorize(Roles = "Admin")]
public class EditModel : PageModel
{
    private readonly IEquipmentRepository _repository;

    [BindProperty] public Domain.entities.Equipment Equipment { get; set; } = new();
    
    public EditModel(IEquipmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var equipment = await _repository.GetByIdAsync(id);
        if (equipment == null) return NotFound();
        
        Equipment = equipment;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        await _repository.UpdateAsync(Equipment);
        return RedirectToPage("Index");
    }
}