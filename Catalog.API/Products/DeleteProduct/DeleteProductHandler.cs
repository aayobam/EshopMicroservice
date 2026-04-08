namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

internal class DeleteProductCommandHandler(ILogger<DeleteProductCommandHandler> logger, IDocumentSession session) 
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.id, cancellationToken);
        
        if (product == null)
        {
            logger.LogError("Product with id {ProductId} not found for deletion.", command.id);
            throw new ProductNotFoundException();
        }

        session.Delete<Product>(product);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}
