using Microsoft.AspNetCore.Mvc.RazorPages;
using EquipmentRental.Domain.entities;
using Microsoft.AspNetCore.Mvc;
using EquipmentRental.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace EquipmentRental.Web.Pages.Equipment;

[Authorize(Roles ="Admin,Employee")]
public class IndexModel : PageModel
{
    private readonly IEquipmentRepository _repository;

    public IEnumerable<Domain.entities.Equipment> EquipmentList { get; set; } = new List<Domain.entities.Equipment>();

    [BindProperty(SupportsGet = true)] public bool ShowAll { get; set; } = false;

    public IndexModel(IEquipmentRepository repository)
    {
        _repository = repository;
    }

    public async Task OnGetAsync()
    {
        EquipmentList = ShowAll ? await _repository.GetAllAsync() : await _repository.GetAvailableAsync();
    }

    
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
        return RedirectToPage("Index");
        
    }

}