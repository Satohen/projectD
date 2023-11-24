using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using discount.Domain;

namespace discount;
public interface ISpecification
{
    string Name { get; }
    IEnumerable<Discount> Process(IEnumerable<Product> products);
}