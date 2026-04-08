namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(Guid id, string Name, string Description, List<string> Category, int Stock, decimal Price);
public record UpdateProductResponse(Product product);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
            .WithName("UpdateProductEndpoint")
            .WithDescription("Endpoint to update product.")
            .WithSummary("Endpoint to update products.")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}
