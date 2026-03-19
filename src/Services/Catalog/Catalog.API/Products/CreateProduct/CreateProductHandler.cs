
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    //Added Validation using FluentValidation
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is requiured");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is requiured");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is requiured");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price Must be greater than 0");
        }
    }

    internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", command);

            //Create Product Entity
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            //Save to Db
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // Return result
            return new CreateProductResult(product.Id);

        }
    }
}
