using System;
using System.Collections.Generic;
using System.Linq;

namespace discount.Domain;

public sealed class BuyMoreBoxesSpecification : ISpecification
{
    public string Name { get;}
    public int Set { get; }
    public decimal PercentOff { get; }
    private List<Discount> _discounts;
    internal BuyMoreBoxesSpecification(
        string name,
        int howManySet,
        int percentOff)
    {
        _discounts = new List<Discount>();
        this.Name = name;
        this.PercentOff = 100 - percentOff;
        this.Set = howManySet;
    }

    public static BuyMoreBoxesSpecification CreateNew(
        string name,
        int howManySet,
        int percentOff)
        => new BuyMoreBoxesSpecification(name, howManySet, percentOff);
    public IEnumerable<Discount> Process(IEnumerable<Product> products)
    {
        var skus = products.GroupBy(
            x => x.Sku,
            (product, info) => new { sku = product, count = info.Count() })
            .Where(x => x.count >= Set);
        foreach (var item in skus)
        {
            int number = item.count / Set;
            var MatchedProducts = products
            .Where(x => x.Sku == item.sku)
            .ToArray();
            decimal price = MatchedProducts.FirstOrDefault().Price;
            _discounts.Add(new Discount(
                MatchedProducts,
                this.Name,
                Math.Round(Set * number * price * PercentOff / 100)));
        }
        return _discounts;
    }
}