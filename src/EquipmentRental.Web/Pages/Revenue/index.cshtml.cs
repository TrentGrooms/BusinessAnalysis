using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EquipmentRental.Application.Interfaces;
using EquipmentRental.Application.services;
using EquipmentRental.Domain.entities;
using static EquipmentRental.Application.services.RentalService;


namespace EquipmentRental.Web.Pages.Revenue;

[Authorize(Roles ="Admin")]
public class Index : PageModel
{
    private readonly RentalService _rentalService;
    public RevenueReport RevenueReport { get; set; } = new RevenueReport();

    public Index(RentalService rentalService)
    {
        _rentalService = rentalService;
    }

    public async Task OnGetAsync()
    {
        RevenueReport = await _rentalService.GetRevenueReportAsync();
    }
}