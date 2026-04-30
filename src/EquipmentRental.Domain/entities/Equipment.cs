namespace EquipmentRental.Domain.entities;

public class Equipment
{
    public int EquipmentId { get; set; }
    public string Name { get; set; } = "";

    public string Category { get; set; } = "";

    public decimal DailyRate { get; set; }

    public EquipmentCondition Condition { get; set; }

    public bool IsAvailable { get; set; }



}
