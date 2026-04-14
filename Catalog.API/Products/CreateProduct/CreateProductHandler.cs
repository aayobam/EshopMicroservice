
using Catalog.API.Exceptions;
using FluentValidation.Results;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, decimal Price, int Stock, string ImageFile) 
    : ICommand<CreateProductResult>;

public record CreateProductResult(Product Product);

internal class CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger, IDocumentSession session) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //var validationResult = await validator.ValidateAsync(command, cancellationToken);
        
        //if (validationResult.Errors.Any())
        //{
        //    var errors = validationResult.Errors.Select(e => e.ErrorMessage).FirstOrDefault();
        //    throw new ValidationException(errors);
        //}

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
        return new CreateProductResult(product);
    }
}
