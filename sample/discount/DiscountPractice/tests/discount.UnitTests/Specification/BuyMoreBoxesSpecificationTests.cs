using discount.Domain;
using discount.UnitTests.Fake;

namespace discount.UnitTests.Specification;

public class BuyMoreBoxesSpecificationTests
{
    public const string UAT="BuyMoreBoxes";

    /// <summary>
    /// 測試複數品項達一定數量時，可獲得特定折扣
    /// </summary>
    [Fact]
    [Trait(UAT,"buy_more_boxes")]
    public void test_double_item_then_get_90_percent_discount()
    {
        // given
        var cart = Cart.CreateNew();
        var fakeItems = new List<Product>(){
            Product.CreateNew("test-11","測試1",100),
            Product.CreateNew("test-22","測試2",200),
            Product.CreateNew("test-11","測試1",100),
            Product.CreateNew("test-31","測試3",300),
        };
        var expectedPrice = fakeItems.Sum(x => x.Price);
        var spec = BuyMoreBoxesSpecification.CreateNew("測試2", 2, 90);
        var expectedAmount = fakeItems
            .Where(x => x.Sku == "test-11")
            .Sum(x => x.Price) * (100 - 90)/100;
        // when
        cart.AddItems(fakeItems);
        cart.AddSpecification(spec);
        cart.CheckOut();
        // then
        Assert.Equal(expectedAmount,cart.TotalDiscount);
        Assert.Equal(expectedPrice-expectedAmount, cart.TotalPrice);
    }
}