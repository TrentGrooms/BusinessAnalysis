using EquipmentRental.Application.services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EquipmentRental.Web.Pages.Revenue;

public class Index : PageModel
{
    private readonly RentalService _rentalService;
    public RevenueReport RevenueReport {get; set;} = new  RevenueReport();

    public Index(RentalService rentalService)
    {
        _rentalService = rentalService;
    }

    public async Task OnGetAsync()
    {
        RevenueReport = await _rentalService.GetRevenueReportAsync();
    }
}