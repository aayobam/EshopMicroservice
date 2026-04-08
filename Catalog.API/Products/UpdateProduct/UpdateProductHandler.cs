namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid id, string Name, string Description, List<string> Category, int Stock, decimal Price) 
    : ICommand<UpdateProductResult>;
public record UpdateProductResult(Product product);

public class UpdateProductCommandHandler(ILogger<UpdateProductCommandHandler> logger, IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.id, cancellationToken);
        
        if (product == null)
        {
            logger.LogError("product not found");

            throw new ProductNotFoundException();
        }
        
        product.Name = command.Name;
        product.Category = command.Category;
        product.Stock = command.Stock;
        product.Price = command.Price;
        product.Description = command.Description;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(product);
    }
}
