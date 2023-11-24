using discount.Domain;

namespace discount.UnitTests.Domain;

public class ProductTests
{
    /// <summary>
    /// 建立品項
    /// </summary>
    [Fact]
    public void test_new_product()
    {
        //arrange
        var name = "[御茶園]特撰冰釀微甜綠茶 550ml(24入)";
        var sku = "test-11111";
        var price = 400;
        //act
        var p = Product.CreateNew(sku,name,price);
        //assert
        Assert.Equal(name, p.Name);
        Assert.Equal(sku, p.Sku);
        Assert.Equal(price, p.Price);
    }
}