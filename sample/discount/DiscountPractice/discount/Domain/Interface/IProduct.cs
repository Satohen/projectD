namespace discount.Domain;

public interface IProduct
{
    string Sku { get; }
    int Id { get; }
    string Name { get; }
    decimal Price { get; }
}