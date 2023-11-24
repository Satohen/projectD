using discount.Domain;
using discount.UnitTests.Fake;

namespace discount.UnitTests.Domain;

public class CartTests
{
    public const string UAT = "Cart";

    /// <summary>
    /// 初始化購物車
    /// </summary>
    [Fact]
    [Trait(UAT,"new_cart")]
    public void test_new_cart()
    {
        // given

        // when
        var cart = Cart.CreateNew();
        // then
        Assert.NotNull(cart);
        Assert.NotNull(cart.Products);
        Assert.NotNull(cart.Specifications);
        Assert.NotNull(cart.Discounts);
    }

    /// <summary>
    /// 測試加入商品至購物車
    /// </summary>
    [Fact]
    [Trait(UAT,"add_item")]
    public void test_add_item_to_cart()
    {
        // given
        var name = "[御茶園]特撰冰釀微甜綠茶 550ml(24入)";
        var sku = "test-11111";
        var price = 400;
        var p = Product.CreateNew(sku, name, price);
        var cart = Cart.CreateNew();
        // when
        cart.AddItem(p);
        // then
        Assert.Contains(p, cart.Products);
    }

    /// <summary>
    /// 測試加入複數商品至購物車
    /// </summary>
    [Fact]
    [Trait(UAT,"add_items")]
    public void test_add_items_to_cart()
    {
        // given
        var cart = Cart.CreateNew();
        var fakeItems = FakeBuilder.FakeItems;
        // when
        cart.AddItems(fakeItems);
        // then
        Assert.Equal(fakeItems, cart.Products);
    }

    /// <summary>
    /// 測試無折扣計價加總
    /// </summary>
    [Fact]
    [Trait(UAT,"checkout")]
    public void test_checkout_without_specifications()
    {
        // given
        var cart = Cart.CreateNew();
        var fakeItems = FakeBuilder.FakeItems;
        cart.AddItems(fakeItems);
        var expectedPrice = fakeItems.Sum(x => x.Price);
        // when
        cart.CheckOut();
        // then
        Assert.Equal( expectedPrice,cart.TotalPrice);
    }

    /// <summary>
    /// 測試購物車加入折扣規則
    /// </summary>
    [Fact]
    [Trait(UAT,"add_specification")]
    public void test_add_specification_to_cart()
    {
        // given
        var cart = Cart.CreateNew();
        var testSpec = new FakeSpecification("測試");
        // when
        cart.AddSpecification(testSpec);
        // then
        var spec = cart.Specifications.FirstOrDefault();
        Assert.Equal(testSpec, spec);
    }

    /// <summary>
    /// 測試有折扣計價加總
    /// </summary>
    [Fact]
    [Trait(UAT, "checkout_with_spec")]
    public void test_checkout_with_specifications()
    {
        // given
        var cart = Cart.CreateNew();
        var fakeItems = FakeBuilder.FakeItems;
        var expectedPrice = fakeItems.Sum(x => x.Price);
        var testSpec = new FakeSpecification("測試");
        var fakeDiscount = testSpec.Process(fakeItems).Sum(x => x.Amount);
        // when
        cart.AddItems(fakeItems);
        cart.AddSpecification(testSpec);
        cart.CheckOut();
        // then
        Assert.Equal(expectedPrice, cart.BeforeDiscountTotalPrice);
        Assert.Equal(fakeDiscount, cart.TotalDiscount);
        Assert.Equal(expectedPrice-fakeDiscount, cart.TotalPrice);
    }
}