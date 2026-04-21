using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EquipmentRental.Domain.entities;
using System.ComponentModel.DataAnnotations;

namespace EquipmentRental.Web.Pages.Account;

public class LoginModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    
    [BindProperty]
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [BindProperty]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    
    public string ErrorMessage { get; set; } = string.Empty;

    public LoginModel(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }
    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        
        var result = await _signInManager.PasswordSignInAsync(
            Email,
            Password,
            isPersistent: false,
            lockoutOnFailure: true);
        
        
        if(result.Succeeded)
            return RedirectToPage("Index");
        
        ErrorMessage = "Invalid email or password";
        return Page();
        
    }
}