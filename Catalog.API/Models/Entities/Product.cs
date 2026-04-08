namespace Catalog.API.Models.Entities;

public class Product: BaseEntity
{
    public string Name { get; set; } = default!;
    public List<String> Category { get; set; } = new();
    public string Description { get; set; } = default!;
    public string? ImageFile { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
