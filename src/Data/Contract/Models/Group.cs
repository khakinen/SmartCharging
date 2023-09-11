namespace SmartCharging.Data.Contract.Models;

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int CapacityInAmps { get; set; }
    public virtual ICollection<ChargeStation> ChargeStations { get; set; } 
}

