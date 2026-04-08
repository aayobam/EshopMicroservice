
namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, decimal Price, int Stock, string ImageFile) 
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id, string Name, string Description, decimal Price,int Stock);

internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product()
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            Price = command.Price,
            Stock = command.Stock,
            ImageFile = command.ImageFile
        };
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id, product.Name, product.Description, product.Price, product.Stock);
    }
}
