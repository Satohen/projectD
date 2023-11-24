using System.Collections.Generic;

namespace discount.Domain.Interface;

public interface ICart
{
    void CheckOut();
    void AddItem(Product product);

    void AddSpecification(ISpecification spec);
    IReadOnlyCollection<Product> Products { get; }

    IReadOnlyCollection<ISpecification> Specifications { get; }
}