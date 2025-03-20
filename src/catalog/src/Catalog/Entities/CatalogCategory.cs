namespace Catalog.Entities;

public class CatalogCategory
{
    public const string TableName = "catalog_category";
    
    public int Id { get; init; }
    public required string Name { get; init; }
    public int? ParentId { get; init; }
    public CatalogCategory? Parent { get; init; }
    public ICollection<CatalogCategory> Children { get; init; } = new List<CatalogCategory>();
}