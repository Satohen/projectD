namespace discount.Domain;
public sealed class Product : IProduct
{
    public int Id { get; private set; }
    public string Sku { get; }
    public string Name { get; }
    public string[] Tags { get; private set; }
    public decimal Price { get; }
    internal Product(string sku, string name, decimal price)
    {
        Sku = sku;
        Name = name;
        Price = price;
    }

    private Product()
    {

    }

    public static Product CreateNew(
        string sku,
        string name,
        decimal price)
        => new Product(sku, name, price);

    public void SetId(int id)
        => Id = id;

    public void UpdateTags(string[] tags)
        => Tags = tags;
}