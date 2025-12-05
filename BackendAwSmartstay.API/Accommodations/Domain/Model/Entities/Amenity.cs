namespace BackendAwSmartstay.API.Accommodations.Domain.Model.Entities;

public class Amenity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = "General"; // Opcional: para agruparlas
}