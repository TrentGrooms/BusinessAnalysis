namespace EquipmentRental.Domain.entities;

public class Rental
{
    public int RentalId { get; set; }
    public int EquipmentId { get; set; }
    public int CustomerId { get; set; }

    public DateTime RentalDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public decimal TotalCost { get; set; }

    public bool IsClosed {get; set; }

    public Equipment Equipment { get; set; }
    public Customer Customer { get; set; }


}
