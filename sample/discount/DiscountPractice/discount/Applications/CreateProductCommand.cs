using discount.Domain;
using discount.Domain.Interface;

namespace discount.Applications;

public class CreateProductCommand
{
    public string Sku { get; set; }
    public string Name { get; set; }
    public string[] Tags { get; set; }
    public decimal Price { get; set; }
}

public class CreateProductCommandHandler : IEventHandler<CreateProductCommand, Product>
{
    private int _id;
    public CreateProductCommandHandler(int id)=>_id = id;
    public Product Handle(CreateProductCommand request)
    {
        var p= Product.CreateNew(request.Sku, request.Name, request.Price);
        p.SetId(_id);
        p.UpdateTags(request.Tags);
        return p;
    }
}