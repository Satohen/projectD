using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace discount.Domain.Specifications;
public sealed class DesignatedItemSpecification : ISpecification
{
    public string Name { get; }
    public int Set { get;}
    public decimal Discount { get;}
    private string DesignatedName;
    internal DesignatedItemSpecification(
        string name,
        string designatedName,
        int itemsInSet,
        decimal discount)
    {
        Name = name;
        this.Set = itemsInSet;
        this.Discount = discount;
        DesignatedName = designatedName;
    }

    public static DesignatedItemSpecification CreateNew(
        string name,
        string designatedName,
        int itemsInSet,
        decimal discount)
        => new DesignatedItemSpecification(name, designatedName, itemsInSet, discount);

    public IEnumerable<Discount> Process(IEnumerable<Product> products)
    {
        var sets = products
        .Where(x => x.Tags.Contains(DesignatedName));
        //   .Select(x=>new {x.tags})
        //   .GroupBy(x=>x.tags,(product,info)=>new {sku=product,count =info.Count()});

        int set = sets.Count() / Set;
        var MatchedProducts = sets.ToArray();
        decimal price = MatchedProducts.FirstOrDefault().Price;
        yield return new Discount(MatchedProducts,Name,Discount);
    }
}