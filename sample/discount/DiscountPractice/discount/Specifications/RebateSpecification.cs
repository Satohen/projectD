using System;
using System.Collections.Generic;
using System.Linq;

namespace discount.Domain;
public sealed class RebateSpecification : ISpecification
{
    public string Name { get;}

    public int MinPrice { get;}

    public int Rebate { get;}

    internal RebateSpecification(string name, int minPrice, int rebate)
        => (Name, MinPrice, Rebate) = (name, minPrice, rebate);

    public static RebateSpecification CreateNew(string name, int minPrice, int rebate)
        => new RebateSpecification(name, minPrice, rebate);

    public IEnumerable<Discount> Process(IEnumerable<Product> products)
    {
        if (products.Sum(x => x.Price) > MinPrice)
        {
            yield return new Discount(products.ToArray(),Name,Rebate);
        }
    }
}