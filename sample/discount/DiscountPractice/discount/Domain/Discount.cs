using discount.Domain;

namespace discount;

public readonly record struct Discount(
    Product[] MatchedProducts,
    string SpecificationName,
    decimal Amount);
// {
//     public  { get;}
//     public  { get;}
//     public  { get;}

//     internal Discount(
//         Product[] matchedProducts,
//         string specificationName,
//         decimal amount)
//     {
//         MatchedProducts = matchedProducts;
//         SpecificationName = specificationName;
//         Amount = amount;
//     }
// }