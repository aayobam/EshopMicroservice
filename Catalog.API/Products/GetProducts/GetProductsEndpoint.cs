namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        //app.MapGroup("/api").WithTags("products");
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
            .WithName("Get Products Endpoint")
            .WithSummary("Get a list of products")
            .WithDescription("This endpoint retrieves a list of products from the catalog.")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}
