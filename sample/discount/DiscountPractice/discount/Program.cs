using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using discount.Domain;
using discount.Domain.Specifications;
using System.Text.Json;
using discount.Applications;

namespace discount
{
  class Program
  {
    static void Main(string[] args)
    {
      var cart = Cart.CreateNew();
      cart.AddItems(LoadProductFromFile(@"./BuyLists/products3.json"));
      cart.AddSpecification(BuyMoreBoxesSpecification.CreateNew("任兩箱結帳88折",2,88));
      cart.AddSpecification(RebateSpecification.CreateNew("滿千折百",1000,100));
      cart.AddSpecification(DesignatedItemSpecification.CreateNew("指定商品 (衛生紙) 一次買 6 捲便宜 100 元","衛生紙",6,100));
      cart.CheckOut();
      foreach (var p in cart.Products)
      {
        Console.WriteLine($"- {p.Id,02}, [{p.Sku}] {p.Price,8:C}, {p.Name} ");
      }
      Console.WriteLine();
      Console.WriteLine($"折扣:");
      foreach(var d in cart.Discounts)
      {
        Console.WriteLine($"{d.MatchedProducts.FirstOrDefault().Name}：{d.Amount}");
      }
      Console.WriteLine($"---------------------------------------------------");
      Console.WriteLine($"折扣金額：{cart.TotalDiscount}");
      Console.WriteLine($"---------------------------------------------------");
      Console.WriteLine($"---------------------------------------------------");
      Console.WriteLine($"結帳金額:   {cart.TotalPrice:C}");
    }

    static int _index = 0;
    public static IEnumerable<Product> LoadProductFromFile(string filePath)
    {
        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive=true };
        var commands = JsonSerializer.Deserialize<CreateProductCommand[]>(File.ReadAllText(filePath), options);
        foreach (var command in commands)
        {
            _index++;
            var handler = new CreateProductCommandHandler(_index);
            yield return handler.Handle(command);
        }
    }
  }


  


}
