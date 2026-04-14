
namespace Catalog.API.Products.CreateProduct;


public record CreateProductRequest(string Name, List<string> Category, string Description, decimal Price, int Stock);
public record CreateProductResponse(Guid Id, string Name, string Description, decimal Price, int Stock);
public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{response.Id}", response);
        })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Create Product")
            .WithDescription("Endpoint to create product.");
    }
}
