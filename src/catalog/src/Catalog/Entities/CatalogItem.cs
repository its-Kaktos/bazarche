namespace Catalog.Entities;

public class CatalogItem
{
    public const string TableName = "catalog_item";

    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public required int AvailableStock { get; init; }
    public required string Slug { get; init; }
    public required int CatalogBrandId { get; init; }
    public required CatalogBrand CatalogBrand { get; init; }
    public required int CatalogCategoryId { get; init; }
    public required CatalogCategory CatalogCategory { get; init; }
}