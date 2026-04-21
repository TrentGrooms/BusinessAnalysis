using Microsoft.AspNetCore.Mvc.RazorPages;
using EquipmentRental.Application.Interfaces;
using EquipmentRental.Domain.entities;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.Web.Pages.Customer;

public class IndexModel : PageModel
{
    private readonly ICustomerRepository _Repository;

    public IEnumerable<Domain.entities.Customer> Customers { get; set; } = new List<Domain.entities.Customer>();
    
    public IndexModel(ICustomerRepository repository)
    {
        _Repository = repository;
    }

    public async Task OnGetAsync()
    {
        Customers = await _Repository.GetAllCustomersAsync();
    }
    
    

}