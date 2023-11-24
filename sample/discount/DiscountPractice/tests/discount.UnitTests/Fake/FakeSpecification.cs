using discount.Domain;

namespace discount.UnitTests.Fake;

public class FakeSpecification : ISpecification
{
    public string Name { get; }

    public FakeSpecification(string name)
    {
        Name = name;
    }

    public IEnumerable<Discount> Process(IEnumerable<Product> products)
        => new List<Discount>(){
            FakeBuilder.FakeDiscount
        };
}