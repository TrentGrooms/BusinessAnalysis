using System.ComponentModel.DataAnnotations;

namespace EquipmentRental.Domain.entities;

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";

    [EmailAddress]
    public string Email { get; set; } = "";

    [Phone]
    public string PhoneNumber { get; set; } = "";

    public bool IsDeleted { get; set; } = false;
}
