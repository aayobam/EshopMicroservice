using Microsoft.AspNetCore.Diagnostics;

namespace Catalog.API.Products.GetProductsByCategory;


public record GetProductsByCategoryResponse(IEnumerable<Product> products);
public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var result = await sender.Send(new GetProductsByCategoryRequest(category));
            var response = result.Adapt<GetProductsByCategoryResponse>();
            return Results.Ok(response);
        })
            .WithName("GetProductByCategory")
            .WithDescription("This endpoint returns products by category.")
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .WithSummary("Get products by category.")
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}
