namespace Catalog.API.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be null or empty.");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be null or empty.");
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than 0.");
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("{PropertyName} must be greater than or equal to 0.");
        RuleFor(x => x.ImageFile)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be null or empty.")
            .Must(AllowedImageExtensions)
            .WithMessage("{PropertyName} has an invalid file extension.");
    }

    private bool AllowedImageExtensions(string image)
    {
        if (string.IsNullOrWhiteSpace(image))
        {
            return false;
        }

        var extension = Path.GetExtension(image);
        {
            if (string.IsNullOrEmpty(extension))
                return false;
        }

        var allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".jpg", ".jpeg", ".png", ".gif" };
        return allowed.Contains(extension);
    }
}
