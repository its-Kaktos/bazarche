namespace Catalog.Entities;

public class CatalogBrand
{
    public const string TableName = "catalog_brand";

    public int Id { get; init; }
    public required string Name { get; init; } 
}