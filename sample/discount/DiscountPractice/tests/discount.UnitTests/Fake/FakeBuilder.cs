using discount.Domain;

namespace discount.UnitTests.Fake;

public class FakeBuilder
{
    public static IEnumerable<Product> FakeItems
        => new List<Product>()
        {
            Product.CreateNew("test-1","測試1",10),
            Product.CreateNew("test-2","測試2",20),
            Product.CreateNew("test-1","測試1",10),
            Product.CreateNew("test-3","測試3",30),
        };

    public static Discount FakeDiscount => new Discount(FakeItems.ToArray(), "測試折扣1", 10);
}