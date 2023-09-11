namespace SmartCharging.Data.Contract.Models;

public class ChargeStation
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid GroupId { get; set; }
    
    public virtual Group Group { get; set; }
    public virtual ICollection<Connector> Connectors { get; set; }
}