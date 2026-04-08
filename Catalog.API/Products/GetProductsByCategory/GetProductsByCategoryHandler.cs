namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductsByCategoryResult(IEnumerable<Product> Products);
public record GetProductsByCategoryRequest(string Category) : IQuery<GetProductsByCategoryResult>;

internal class GetProductsByCategoryQueryHandler(ILogger<GetProductsByCategoryQueryHandler> logger, IDocumentSession session)
    : IQueryHandler<GetProductsByCategoryRequest, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetProductsByCategoryRequest for category: {Category}", request.Category);
        
        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(request.Category))
            .ToListAsync(cancellationToken);

        if (products == null || !products.Any())
        {
            logger.LogWarning("No products found for category: {Category}", request.Category);
            return new GetProductsByCategoryResult(Enumerable.Empty<Product>());
        }

        return new GetProductsByCategoryResult(products);
    }
}
